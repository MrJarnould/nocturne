namespace Nocturne.Core.Contracts.Multitenancy;

public interface IMemberInviteService
{
    /// <summary>Creates a new invite link that grants the specified roles and permissions when accepted.</summary>
    Task<MemberInviteResult> CreateInviteAsync(
        Guid tenantId,
        Guid createdBySubjectId,
        List<Guid> roleIds,
        List<string>? directPermissions = null,
        string? label = null,
        int expiresInDays = 7,
        int? maxUses = null,
        bool limitTo24Hours = false);

    /// <summary>Retrieves invite details by token, or null if the token is invalid or expired.</summary>
    Task<MemberInviteInfo?> GetInviteByTokenAsync(string token);

    /// <summary>Accepts an invite and adds the subject as a member of the tenant.</summary>
    Task<AcceptMemberInviteResult> AcceptInviteAsync(string token, Guid acceptingSubjectId);

    /// <summary>Returns all invites for the specified tenant, including usage history.</summary>
    Task<List<MemberInviteInfo>> GetInvitesForTenantAsync(Guid tenantId);

    /// <summary>Revokes an invite so it can no longer be accepted.</summary>
    Task<bool> RevokeInviteAsync(Guid inviteId, Guid tenantId);
}

public record MemberInviteResult(
    Guid Id,
    string Token,
    string InviteUrl,
    DateTime ExpiresAt);

public record MemberInviteInfo(
    Guid Id,
    Guid TenantId,
    string TenantName,
    string CreatedByName,
    List<Guid> RoleIds,
    List<string>? DirectPermissions,
    string? Label,
    bool LimitTo24Hours,
    DateTime ExpiresAt,
    int? MaxUses,
    int UseCount,
    bool IsValid,
    bool IsExpired,
    bool IsRevoked,
    DateTime CreatedAt,
    List<InviteUsageInfo> UsedBy);

public record InviteUsageInfo(
    Guid SubjectId,
    string? Name,
    DateTime JoinedAt);

public record AcceptMemberInviteResult(
    bool Success,
    string? ErrorCode = null,
    string? ErrorDescription = null,
    Guid? MembershipId = null);
