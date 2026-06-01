using Nocturne.Core.Models.V4;

namespace Nocturne.Core.Contracts.V4;

/// <summary>
/// Manages <see cref="ConsumableInstance"/> wear sessions. The hook called by
/// <c>DeviceEventController</c> and <c>TreatmentDecomposer</c> after every
/// successful <see cref="DeviceEvent"/> write decides whether to close the
/// prior open instance and open a new one based on the event's
/// <see cref="DeviceEvent.EventType"/> and the patient's current
/// <see cref="PatientDevice"/>s.
/// </summary>
public interface IConsumableInstanceService
{
    /// <summary>
    /// Open / close wear sessions in response to a newly-persisted DeviceEvent.
    /// Idempotent: re-handling the same event (matched by
    /// <see cref="DeviceEvent.Id"/>) is a no-op.
    /// </summary>
    /// <remarks>
    /// Event-type semantics (Phase 1):
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <see cref="DeviceEventType.SensorStart"/>: close the previous
    ///       <see cref="ConsumableKind.CgmSensor"/> instance (if any) and open
    ///       a new one. Catalog resolution is driven by the patient's current
    ///       CGM <see cref="PatientDevice"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <see cref="DeviceEventType.SiteChange"/>: same pattern, but for
    ///       pods (tubeless pumps) or infusion sets (tubed pumps) depending on
    ///       <see cref="PumpCapability.IsTubeless"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>All other event types: no-op.</description>
    ///   </item>
    /// </list>
    /// </remarks>
    Task HandleDeviceEventAsync(DeviceEvent deviceEvent, CancellationToken ct = default);

    /// <summary>Returns the open instances for the current tenant.</summary>
    Task<IReadOnlyList<ConsumableInstance>> GetActiveAsync(CancellationToken ct = default);

    /// <summary>Returns the most recent N closed instances across all kinds.</summary>
    Task<IReadOnlyList<ConsumableInstance>> GetRecentClosedAsync(int limit, CancellationToken ct = default);

    /// <summary>Returns an instance by id, or null.</summary>
    Task<ConsumableInstance?> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Patches the editable fields of an instance (insertion site, end reason,
    /// serial, notes, ended-at, residual units). Returns the updated record or
    /// null when not found.
    /// </summary>
    Task<ConsumableInstance?> UpdateAsync(Guid id, ConsumableInstanceEditRequest patch, CancellationToken ct = default);

    /// <summary>Soft-deletes an instance.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}

/// <summary>
/// Editable subset of a <see cref="ConsumableInstance"/>. Identity, FKs to
/// catalog / device / inventory are immutable from this surface.
/// </summary>
public record ConsumableInstanceEditRequest(
    string? InsertionSite,
    string? SerialNumber,
    string? Notes,
    DateTime? EndedAt,
    ConsumableInstanceEndReason? EndReason,
    decimal? ResidualUnits);
