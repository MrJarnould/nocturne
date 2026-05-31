namespace Nocturne.Core.Models.V4;

/// <summary>
/// A SKU-level consumable that belongs to a <see cref="DeviceCatalogEntry"/>.
/// One device model can have multiple consumables — a Dexcom G6 has both a
/// sensor and a (disposable) transmitter; a t:slim X2 has a reservoir, an
/// infusion set, and a separately-sold cannula. Inventory items reference
/// consumables by <see cref="Id"/>; the device-level capabilities
/// (<see cref="CgmCapability"/>, <see cref="PumpCapability"/>) carry physical
/// properties that apply to the model, while these per-SKU rows carry what
/// you buy and stock.
/// </summary>
/// <remarks>
/// Catalog entries are append-only. A manufacturer changing specs mid-product
/// (G7 → G7 15-day) must result in a new <see cref="DeviceCatalogEntry"/> ID
/// and new consumable IDs — never an in-place edit of an existing entry.
/// Historical inventory data depends on this invariant.
/// </remarks>
/// <seealso cref="ConsumableCatalog"/>
/// <seealso cref="ConsumableKind"/>
/// <seealso cref="DeviceCatalogEntry"/>
public record ConsumableCatalogEntry
{
    /// <summary>
    /// Unique kebab-case identifier for this consumable SKU
    /// (e.g., "dexcom-g6-sensor", "omnipod-5-pod", "tslim-x2-infusion-set").
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// FK to the parent <see cref="DeviceCatalogEntry.Id"/>. Every consumable
    /// belongs to exactly one device model.
    /// </summary>
    public required string DeviceCatalogId { get; init; }

    /// <summary>
    /// Role of this consumable (sensor, transmitter, pod, etc.). Drives
    /// inventory categorisation and which auto-consume hooks apply.
    /// </summary>
    public required ConsumableKind Kind { get; init; }

    /// <summary>
    /// Human-readable display name (e.g., "Dexcom G6 sensor",
    /// "Omnipod 5 pod"). Used wherever the SKU is shown to the patient.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Singular/plural unit shown in the UI ("sensors", "pods",
    /// "cartridges", "sets", "cannulas").
    /// </summary>
    public required string UnitLabel { get; init; }

    /// <summary>
    /// Maximum wear duration in days. Drives deterministic run-out
    /// projection. Null for consumables that are time-of-use only
    /// (none today; reserved for future cases).
    /// </summary>
    public int? WearDays { get; init; }

    /// <summary>
    /// Physical cannula specification when this consumable carries an
    /// integrated or separate cannula. Set on <see cref="ConsumableKind.Pod"/>
    /// (integrated) and <see cref="ConsumableKind.InfusionSet"/> /
    /// <see cref="ConsumableKind.Cannula"/> (separate) entries.
    /// </summary>
    public CannulaSpec? Cannula { get; init; }
}
