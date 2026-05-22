using Microsoft.EntityFrameworkCore;
using Nocturne.Core.Constants;
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Core.Models.Authorization;
using Nocturne.Infrastructure.Data;

namespace Nocturne.API.Services.Multitenancy;

/// <summary>
/// DB-backed implementation of <see cref="ITenantOwnerResolver"/>. Looks up the
/// owner subject for the current tenant via the <see cref="ITenantAccessor"/>.
/// </summary>
public sealed class TenantOwnerResolver : ITenantOwnerResolver
{
    private readonly ITenantAccessor _tenantAccessor;
    private readonly IDbContextFactory<NocturneDbContext> _contextFactory;

    public TenantOwnerResolver(
        ITenantAccessor tenantAccessor,
        IDbContextFactory<NocturneDbContext> contextFactory)
    {
        _tenantAccessor = tenantAccessor;
        _contextFactory = contextFactory;
    }

    public async Task<string?> GetCurrentTenantOwnerSubjectIdAsync(CancellationToken ct = default)
    {
        var tenantId = _tenantAccessor.TenantId;
        if (tenantId == Guid.Empty) return null;

        await using var ctx = await _contextFactory.CreateDbContextAsync(ct);
        var ownerSubjectId = await ctx.TenantMembers.AsNoTracking()
            .Where(tm => tm.TenantId == tenantId
                && tm.MemberRoles.Any(mr => mr.TenantRole.Slug == TenantPermissions.SeedRoles.Owner))
            .Select(tm => tm.SubjectId)
            .FirstOrDefaultAsync(ct);

        return ownerSubjectId == Guid.Empty ? null : ownerSubjectId.ToString();
    }
}
