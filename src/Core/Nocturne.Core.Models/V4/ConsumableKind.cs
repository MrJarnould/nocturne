using System.Text.Json.Serialization;

namespace Nocturne.Core.Models.V4;

/// <summary>
/// Categorises the role a consumable plays so inventory and analytics can
/// reason about it without knowing the specific SKU. The set is intentionally
/// small and stable; introduce a new kind only when the existing ones can't
/// be honestly fit to the part.
/// </summary>
/// <seealso cref="ConsumableCatalogEntry"/>
[JsonConverter(typeof(JsonStringEnumConverter<ConsumableKind>))]
public enum ConsumableKind
{
    /// <summary>The disposable glucose sensor worn by the patient.</summary>
    CgmSensor,

    /// <summary>
    /// A disposable separate transmitter (e.g. Dexcom G6). Rechargeable
    /// transmitters (Eversense) are durables, not consumables.
    /// </summary>
    CgmTransmitter,

    /// <summary>An all-in-one tubeless pump unit (e.g. Omnipod).</summary>
    Pod,

    /// <summary>An insulin cartridge for a tubed pump (e.g. t:slim, MiniMed).</summary>
    Reservoir,

    /// <summary>
    /// Tubing + cannula assembly that connects a tubed pump to the body.
    /// For pump systems where the manufacturer sells the set as a single
    /// SKU (Medtronic), this is the only set-related consumable. For systems
    /// that mix tubing bodies and cannulas independently (Tandem), this is
    /// the body and <see cref="Cannula"/> is the separate cannula SKU.
    /// </summary>
    InfusionSet,

    /// <summary>
    /// A standalone replacement cannula. Present only for systems where the
    /// manufacturer sells cannulas as a separate SKU from the infusion set
    /// body (Tandem yes, Medtronic no).
    /// </summary>
    Cannula
}
