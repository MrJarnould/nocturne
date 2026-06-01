using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Core.Contracts.V4;
using Nocturne.Core.Contracts.V4.Repositories;
using Nocturne.Core.Models;
using Nocturne.Infrastructure.Data;
using Nocturne.Infrastructure.Data.Mappers.V4;

namespace Nocturne.API.Services.BackgroundServices;

/// <summary>
/// One-shot per-tenant backfill that replays historical SensorStart and
/// SiteChange device events through the consumable-instance hook so existing
/// tenants get a populated wear-session history when Phase 1 ships.
/// </summary>
/// <remarks>
/// Idempotency:
/// <list type="bullet">
///   <item>
///     <description>
///       Tenant level: skips any tenant where <see cref="IConsumableInstanceRepository.AnyExistAsync"/>
///       already returns true. Once a tenant has any instance, the live hook has
///       taken over and the backfill should not run again.
///     </description>
///   </item>
///   <item>
///     <description>
///       Event level: the hook itself short-circuits on
///       <see cref="ConsumableInstance.SourceDeviceEventId"/>, so even if the
///       tenant-level guard is bypassed (e.g. partial backfill from a prior
///       crash plus a single manually-opened instance), no duplicate session
///       is opened.
///     </description>
///   </item>
/// </list>
/// </remarks>
public class ConsumableInstanceBackfillService(
    IServiceProvider serviceProvider,
    ILogger<ConsumableInstanceBackfillService> logger) : BackgroundService
{
    // Wait long enough that the API is warm and EF migrations have settled
    // before we start touching tenant data.
    private static readonly TimeSpan InitialDelay = TimeSpan.FromSeconds(30);
    private const int PageSize = 500;

    // Persisted as strings in device_events.event_type; compare on the string form.
    private static readonly string[] ReplayEventTypeNames =
    [
        nameof(DeviceEventType.SensorStart),
        nameof(DeviceEventType.SiteChange),
    ];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.Delay(InitialDelay, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        try
        {
            await BackfillAllTenantsAsync(stoppingToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            logger.LogWarning(ex, "Consumable-instance backfill failed; will retry on next process restart");
        }
    }

    internal async Task BackfillAllTenantsAsync(CancellationToken ct)
    {
        using var lookupScope = serviceProvider.CreateScope();
        var factory = lookupScope.ServiceProvider.GetRequiredService<IDbContextFactory<NocturneDbContext>>();
        await using var lookupContext = await factory.CreateDbContextAsync(ct);
        var tenants = await lookupContext.Tenants.AsNoTracking()
            .Where(t => t.IsActive)
            .Select(t => new { t.Id, t.Slug, t.DisplayName })
            .ToListAsync(ct);

        foreach (var tenant in tenants)
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                await BackfillTenantAsync(tenant.Id, tenant.Slug, tenant.DisplayName, ct);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                logger.LogWarning(ex,
                    "Consumable-instance backfill failed for tenant {TenantSlug}; continuing with next tenant",
                    tenant.Slug);
            }
        }
    }

    private async Task BackfillTenantAsync(Guid tenantId, string slug, string displayName, CancellationToken ct)
    {
        using var scope = serviceProvider.CreateScope();
        var tenantAccessor = scope.ServiceProvider.GetRequiredService<ITenantAccessor>();
        tenantAccessor.SetTenant(new TenantContext(tenantId, slug, displayName, IsActive: true));

        var repo = scope.ServiceProvider.GetRequiredService<IConsumableInstanceRepository>();
        if (await repo.AnyExistAsync(ct))
        {
            logger.LogDebug(
                "Tenant {TenantSlug} already has consumable_instances rows; skipping backfill",
                slug);
            return;
        }

        var service = scope.ServiceProvider.GetRequiredService<IConsumableInstanceService>();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<NocturneDbContext>>();

        var totalReplayed = 0;
        var lastTimestamp = DateTime.MinValue;

        while (true)
        {
            ct.ThrowIfCancellationRequested();
            await using var db = await contextFactory.CreateDbContextAsync(ct);
            db.TenantId = tenantId; // RLS GUC is set via the interceptor when the connection opens.

            var batch = await db.DeviceEvents
                .AsNoTracking()
                .Where(e => ReplayEventTypeNames.Contains(e.EventType))
                .Where(e => e.Timestamp > lastTimestamp)
                .OrderBy(e => e.Timestamp).ThenBy(e => e.Id)
                .Take(PageSize)
                .ToListAsync(ct);

            if (batch.Count == 0) break;

            foreach (var entity in batch)
            {
                var model = DeviceEventMapper.ToDomainModel(entity);
                await service.HandleDeviceEventAsync(model, ct);
                lastTimestamp = entity.Timestamp;
                totalReplayed++;
            }
        }

        if (totalReplayed > 0)
        {
            logger.LogInformation(
                "Consumable-instance backfill replayed {Count} historical events for tenant {TenantSlug}",
                totalReplayed, slug);
        }
    }
}
