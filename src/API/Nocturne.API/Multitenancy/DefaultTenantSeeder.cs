using Microsoft.EntityFrameworkCore;
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Core.Models.Authorization;
using Nocturne.Infrastructure.Data;
using Nocturne.Infrastructure.Data.Entities;

namespace Nocturne.API.Multitenancy;

/// <summary>
/// Startup service that ensures a default tenant exists and backfills
/// tenant_id on any existing data rows that predate multitenancy.
/// </summary>
public static class DefaultTenantSeeder
{
    public static async Task SeedDefaultTenantAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<NocturneDbContext>>();
        await using var context = await factory.CreateDbContextAsync();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<NocturneDbContext>>();

        // Check if any tenants exist
        var tenantCount = await context.Tenants.CountAsync();
        if (tenantCount > 0)
        {
            logger.LogDebug("Tenants already exist ({Count}), skipping default tenant seeding", tenantCount);
            return;
        }

        logger.LogInformation("No tenants found. Creating default tenant and backfilling tenant_id...");

        // Create default tenant
        var defaultTenant = new TenantEntity
        {
            Slug = "default",
            DisplayName = "Default",
            IsActive = true,
            IsDefault = true,
        };
        context.Tenants.Add(defaultTenant);
        await context.SaveChangesAsync();

        var tenantId = defaultTenant.Id;
        logger.LogInformation("Created default tenant with ID {TenantId}", tenantId);

        // Seed default roles for the tenant
        var roleService = scope.ServiceProvider.GetRequiredService<ITenantRoleService>();
        await roleService.SeedRolesForTenantAsync(tenantId);

        // Create Public subject membership (no roles = unconfigured sentinel)
        var publicSubject = await context.Subjects
            .FirstOrDefaultAsync(s => s.IsSystemSubject && s.Name == "Public");

        if (publicSubject != null)
        {
            var publicMember = new TenantMemberEntity
            {
                Id = Guid.CreateVersion7(),
                TenantId = tenantId,
                SubjectId = publicSubject.Id,
                LimitTo24Hours = true,
                Label = "Public Access",
                SysCreatedAt = DateTime.UtcNow,
                SysUpdatedAt = DateTime.UtcNow,
            };
            context.TenantMembers.Add(publicMember);
            await context.SaveChangesAsync();
            logger.LogInformation("Created Public subject membership for tenant {TenantId}", tenantId);
        }
        else
        {
            logger.LogWarning("Public system subject not found — skipping public access membership for tenant {TenantId}", tenantId);
        }

        var ownerRole = await context.TenantRoles
            .FirstAsync(r => r.TenantId == tenantId && r.Slug == TenantPermissions.SeedRoles.Owner);

        // Assign all existing subjects as owners of the default tenant
        var subjects = await context.Subjects.Where(s => !s.IsSystemSubject).ToListAsync();
        foreach (var subject in subjects)
        {
            var member = new TenantMemberEntity
            {
                Id = Guid.CreateVersion7(),
                TenantId = tenantId,
                SubjectId = subject.Id,
                SysCreatedAt = DateTime.UtcNow,
                SysUpdatedAt = DateTime.UtcNow,
            };
            context.TenantMembers.Add(member);
            context.TenantMemberRoles.Add(new TenantMemberRoleEntity
            {
                Id = Guid.CreateVersion7(),
                TenantMemberId = member.Id,
                TenantRoleId = ownerRole.Id,
                SysCreatedAt = DateTime.UtcNow,
            });
        }
        await context.SaveChangesAsync();
        logger.LogInformation("Assigned {Count} existing subjects to default tenant", subjects.Count);

        // Previously this seeder also walked a hardcoded list of tenant-scoped
        // tables and backfilled tenant_id on any NULL rows. That work was only
        // needed during the original multitenancy rollout and ran in the same
        // transaction as the 20260227034745_EnforceMultitenancy migration.
        //
        // The loop was removed because:
        //   (1) It only runs when zero tenants exist, which on a fresh install
        //       means the tenant-scoped tables are also empty.
        //   (2) RLS is now enabled + forced on every tenant-scoped table. Any
        //       UPDATE issued here would run without app.current_tenant_id set
        //       and be rejected by the tenant_isolation policy.
        //   (3) The hardcoded table list had drifted — it still referenced
        //       alert_rules / alert_history, which were dropped in
        //       20260322031413_DropOldAlertTables.

        logger.LogInformation("Default tenant seeding complete");
    }
}
