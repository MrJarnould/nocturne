namespace Nocturne.Core.Contracts.Multitenancy;

/// <summary>
/// Resolves the subject ID of a tenant's owner. Used by tenant-wide services
/// that need to route user-keyed primitives (e.g. in-app notifications) to a
/// specific recipient when no individual subject is implied by the operation.
/// </summary>
public interface ITenantOwnerResolver
{
    /// <summary>
    /// Returns the owner subject ID for the current tenant context (as resolved
    /// by <see cref="ITenantAccessor"/>) as a string, or <see langword="null"/>
    /// when the tenant context is not set or no owner exists.
    /// </summary>
    Task<string?> GetCurrentTenantOwnerSubjectIdAsync(CancellationToken ct = default);
}
