using System.Text.Json.Serialization;

namespace Nocturne.Core.Models.V4;

/// <summary>
/// Why a <see cref="ConsumableInstance"/> stopped being worn. The vocabulary
/// is small and patient-oriented; granular failure modes (occlusion sub-type,
/// adhesion failure mode, etc.) belong in
/// <see cref="ConsumableInstance.Notes"/>.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ConsumableInstanceEndReason>))]
public enum ConsumableInstanceEndReason
{
    /// <summary>
    /// Replaced at the end of its planned wear window. The expected case.
    /// </summary>
    Planned,

    /// <summary>
    /// Removed early because of an occlusion alarm or visible blockage.
    /// </summary>
    Occlusion,

    /// <summary>
    /// Failed mid-wear for any non-occlusion reason (kinked cannula, bad
    /// sensor, broken pod, etc.).
    /// </summary>
    Failure,

    /// <summary>
    /// Came off the body unintentionally before the planned end (adhesive
    /// failure, snagged on clothing, etc.).
    /// </summary>
    FellOff,

    /// <summary>
    /// Catch-all when the patient knows the instance ended but isn't sure
    /// which of the above applies.
    /// </summary>
    Unknown
}
