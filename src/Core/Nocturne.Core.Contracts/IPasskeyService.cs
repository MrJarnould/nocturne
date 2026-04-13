namespace Nocturne.Core.Contracts;

/// <summary>
/// Service for managing WebAuthn/FIDO2 passkey authentication.
/// </summary>
public interface IPasskeyService
{
    /// <summary>Generates registration options for creating a new passkey credential.</summary>
    Task<PasskeyRegistrationOptions> GenerateRegistrationOptionsAsync(Guid subjectId, string username, Guid tenantId);

    /// <summary>Validates the attestation response and stores the new passkey credential.</summary>
    Task<PasskeyCredentialResult> CompleteRegistrationAsync(string attestationResponseJson, string challengeToken, Guid tenantId);

    /// <summary>Generates assertion options for a discoverable (usernameless) login flow.</summary>
    Task<PasskeyAssertionOptions> GenerateDiscoverableAssertionOptionsAsync(Guid tenantId);

    /// <summary>Generates assertion options for a username-based login flow.</summary>
    Task<PasskeyAssertionOptions> GenerateAssertionOptionsAsync(string username, Guid tenantId);

    /// <summary>Validates the assertion response and returns the authenticated subject.</summary>
    Task<PasskeyAssertionResult> CompleteAssertionAsync(string assertionResponseJson, string challengeToken, Guid tenantId);

    /// <summary>Returns all registered passkey credentials for the specified subject.</summary>
    Task<List<PasskeyCredentialInfo>> GetCredentialsAsync(Guid subjectId, Guid tenantId);

    /// <summary>Removes a passkey credential from the specified subject.</summary>
    Task RemoveCredentialAsync(Guid credentialId, Guid subjectId, Guid tenantId);

    /// <summary>Returns the number of passkey credentials registered to the specified subject.</summary>
    Task<int> GetCredentialCountAsync(Guid subjectId, Guid tenantId);
}

public record PasskeyRegistrationOptions(string OptionsJson, string ChallengeToken);
public record PasskeyAssertionOptions(string OptionsJson, string ChallengeToken);
public record PasskeyAssertionResult(Guid SubjectId, string Username, string DisplayName);
public record PasskeyCredentialResult(Guid CredentialId, Guid SubjectId);
public record PasskeyCredentialInfo(Guid Id, string? Label, DateTime CreatedAt, DateTime? LastUsedAt, Guid? AaGuid);
