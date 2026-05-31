namespace Nocturne.Core.Models.V4;

/// <summary>
/// A known device model in the <see cref="DeviceCatalog"/>. Capabilities are
/// expressed compositionally — a Dexcom G7 is a CGM (<see cref="Cgm"/> set), an
/// Omnipod 5 is a Pump + AID (<see cref="Pump"/> and <see cref="Aid"/> set), and
/// (hypothetically) an integrated pump+CGM device could populate both. The
/// optional capability records carry all model-specific physical / pharmacological
/// data; <see cref="DeviceCatalogEntry"/> itself only holds identity and
/// catalog-level metadata.
/// </summary>
/// <seealso cref="DeviceCatalog"/>
/// <seealso cref="DeviceCategory"/>
/// <seealso cref="CgmCapability"/>
/// <seealso cref="PumpCapability"/>
/// <seealso cref="AidCapability"/>
/// <seealso cref="PatientDevice"/>
/// <seealso cref="ConsumableCatalogEntry"/>
public record DeviceCatalogEntry
{
    /// <summary>
    /// Unique kebab-case identifier for this device model (e.g., "omnipod-5", "dexcom-g7").
    /// Used as the <see cref="PatientDevice.CatalogId"/> reference and as the
    /// <see cref="ConsumableCatalogEntry.DeviceCatalogId"/> foreign key.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Human-readable display name of the device model (e.g., "Omnipod 5", "Dexcom G7").
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Manufacturer name (e.g., "Insulet", "Dexcom", "Abbott").
    /// </summary>
    public required string Manufacturer { get; init; }

    /// <summary>
    /// Primary category for UI grouping (Pump / CGM / Meter / Pen / etc.).
    /// This is presentation-only — the authoritative source of what the device
    /// *does* is the set of populated capability records below. A device with
    /// both <see cref="Pump"/> and <see cref="Cgm"/> capabilities can still
    /// carry a single <see cref="PrimaryCategory"/> for the UI grouping it
    /// shows up under.
    /// </summary>
    /// <seealso cref="DeviceCategory"/>
    public required DeviceCategory PrimaryCategory { get; init; }

    /// <summary>
    /// Set when the device acts as a continuous glucose monitor.
    /// </summary>
    public CgmCapability? Cgm { get; init; }

    /// <summary>
    /// Set when the device delivers insulin (pump). Distinguishes tubeless
    /// pods from tubed reservoir pumps and carries reservoir capacity used
    /// for inventory's linked-insulin drain projection.
    /// </summary>
    public PumpCapability? Pump { get; init; }

    /// <summary>
    /// Set when the device ships with a built-in Automated Insulin Delivery
    /// algorithm (Omnipod 5, MiniMed 780G, t:slim X2 Control-IQ).
    /// </summary>
    public AidCapability? Aid { get; init; }
}
