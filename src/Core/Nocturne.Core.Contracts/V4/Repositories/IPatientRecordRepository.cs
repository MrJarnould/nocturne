using Nocturne.Core.Models.V4;

namespace Nocturne.Core.Contracts.V4.Repositories;

public interface IPatientRecordRepository
{
    /// <summary>Returns the patient record for the current tenant, or null if none exists.</summary>
    Task<PatientRecord?> GetAsync(CancellationToken ct = default);

    /// <summary>Returns the patient record for the current tenant, creating a default one if none exists.</summary>
    Task<PatientRecord> GetOrCreateAsync(CancellationToken ct = default);

    /// <summary>Updates the patient record and returns the saved result.</summary>
    Task<PatientRecord> UpdateAsync(PatientRecord model, CancellationToken ct = default);
}
