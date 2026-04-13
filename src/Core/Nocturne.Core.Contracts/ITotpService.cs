namespace Nocturne.Core.Contracts;

/// <summary>
/// Service for managing TOTP (Time-based One-Time Password) two-factor authentication.
/// </summary>
public interface ITotpService
{
    /// <summary>Generates a TOTP secret and provisioning URI for scanning by an authenticator app.</summary>
    Task<TotpSetupResult> GenerateSetupAsync(Guid subjectId, string username);

    /// <summary>Verifies a TOTP code against the pending setup challenge and registers the credential.</summary>
    Task<TotpCredentialResult> CompleteSetupAsync(string code, string label, string challengeToken);

    /// <summary>Verifies a TOTP code for login and returns the authenticated subject, or null if invalid.</summary>
    Task<TotpLoginResult?> VerifyLoginAsync(string username, string code);

    /// <summary>Returns all registered TOTP credentials for the specified subject.</summary>
    Task<List<TotpCredentialInfo>> GetCredentialsAsync(Guid subjectId);

    /// <summary>Removes a TOTP credential from the specified subject.</summary>
    Task RemoveCredentialAsync(Guid credentialId, Guid subjectId);

    /// <summary>Returns the number of TOTP credentials registered to the specified subject.</summary>
    Task<int> GetCredentialCountAsync(Guid subjectId);
}

public record TotpSetupResult(string ProvisioningUri, string Base32Secret, string ChallengeToken);
public record TotpCredentialResult(Guid CredentialId, Guid SubjectId);
public record TotpLoginResult(Guid SubjectId, string Username, string DisplayName);
public record TotpCredentialInfo(Guid Id, string? Label, DateTime CreatedAt, DateTime? LastUsedAt);
