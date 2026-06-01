using Microsoft.EntityFrameworkCore;
using Nocturne.Core.Contracts.V4.Repositories;
using Nocturne.Core.Models.V4;
using Nocturne.Infrastructure.Data.Mappers.V4;
using Nocturne.Infrastructure.Data.Services;

namespace Nocturne.Infrastructure.Data.Repositories.V4;

/// <inheritdoc cref="IConsumableInstanceRepository"/>
public class ConsumableInstanceRepository : IConsumableInstanceRepository
{
    private readonly ITenantDbContextFactory _contextFactory;

    public ConsumableInstanceRepository(ITenantDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ConsumableInstance?> GetOpenByKindAsync(ConsumableKind kind, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var kindStr = kind.ToString();
        var entity = await ctx.ConsumableInstances
            .AsNoTracking()
            .Where(e => e.Kind == kindStr && e.EndedAt == null)
            .OrderByDescending(e => e.StartedAt)
            .FirstOrDefaultAsync(ct);
        return entity is null ? null : ConsumableInstanceMapper.ToDomainModel(entity);
    }

    public async Task<IReadOnlyList<ConsumableInstance>> GetAllOpenAsync(CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var entities = await ctx.ConsumableInstances
            .AsNoTracking()
            .Where(e => e.EndedAt == null)
            .OrderByDescending(e => e.StartedAt)
            .ToListAsync(ct);
        return entities.Select(ConsumableInstanceMapper.ToDomainModel).ToList();
    }

    public async Task<IReadOnlyList<ConsumableInstance>> GetRecentClosedAsync(
        ConsumableKind kind, int limit, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var kindStr = kind.ToString();
        var entities = await ctx.ConsumableInstances
            .AsNoTracking()
            .Where(e => e.Kind == kindStr && e.EndedAt != null)
            .OrderByDescending(e => e.EndedAt)
            .Take(limit)
            .ToListAsync(ct);
        return entities.Select(ConsumableInstanceMapper.ToDomainModel).ToList();
    }

    public async Task<IReadOnlyList<ConsumableInstance>> GetRecentClosedAsync(int limit, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var entities = await ctx.ConsumableInstances
            .AsNoTracking()
            .Where(e => e.EndedAt != null)
            .OrderByDescending(e => e.EndedAt)
            .Take(limit)
            .ToListAsync(ct);
        return entities.Select(ConsumableInstanceMapper.ToDomainModel).ToList();
    }

    public async Task<ConsumableInstance?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var entity = await ctx.ConsumableInstances.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, ct);
        return entity is null ? null : ConsumableInstanceMapper.ToDomainModel(entity);
    }

    public async Task<ConsumableInstance?> GetBySourceDeviceEventIdAsync(Guid deviceEventId, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var entity = await ctx.ConsumableInstances
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.SourceDeviceEventId == deviceEventId, ct);
        return entity is null ? null : ConsumableInstanceMapper.ToDomainModel(entity);
    }

    public async Task<ConsumableInstance> CreateAsync(ConsumableInstance instance, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var entity = ConsumableInstanceMapper.ToEntity(instance);
        ctx.ConsumableInstances.Add(entity);
        await ctx.SaveChangesAsync(ct);
        return ConsumableInstanceMapper.ToDomainModel(entity);
    }

    public async Task<ConsumableInstance?> UpdateAsync(ConsumableInstance instance, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var existing = await ctx.ConsumableInstances.FirstOrDefaultAsync(e => e.Id == instance.Id, ct);
        if (existing is null) return null;
        ConsumableInstanceMapper.UpdateEntity(existing, instance);
        await ctx.SaveChangesAsync(ct);
        return ConsumableInstanceMapper.ToDomainModel(existing);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        var existing = await ctx.ConsumableInstances.FirstOrDefaultAsync(e => e.Id == id, ct);
        if (existing is null) return false;
        existing.DeletedAt = DateTime.UtcNow;
        existing.SysUpdatedAt = DateTime.UtcNow;
        await ctx.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> AnyExistAsync(CancellationToken ct = default)
    {
        await using var ctx = await _contextFactory.CreateAsync(ct);
        return await ctx.ConsumableInstances.AnyAsync(ct);
    }
}
