namespace Nocturne.Core.Models.V4;

/// <summary>
/// Pump capability composed onto a <see cref="DeviceCatalogEntry"/>. Describes
/// the physical characteristics of the insulin delivery hardware. Tubeless
/// pumps (pods) share a single discardable unit; tubed pumps have separate
/// reservoirs and infusion sets.
/// </summary>
/// <seealso cref="DeviceCatalogEntry"/>
/// <seealso cref="ConsumableCatalogEntry"/>
public record PumpCapability
{
    /// <summary>
    /// Whether the pump is tubeless (a worn pod, e.g. Omnipod) vs. a tubed
    /// pump connected to an infusion set (e.g. t:slim, MiniMed, YpsoPump).
    /// Drives which <see cref="ConsumableKind"/> values apply: tubeless →
    /// <see cref="ConsumableKind.Pod"/>; tubed →
    /// <see cref="ConsumableKind.Reservoir"/> + <see cref="ConsumableKind.InfusionSet"/>.
    /// </summary>
    public required bool IsTubeless { get; init; }

    /// <summary>
    /// Maximum insulin capacity of the reservoir or pod in units.
    /// Used by inventory to model linked-insulin drain per pump-site change
    /// (e.g. 200 u for Omnipod, 300 u for t:slim X2).
    /// </summary>
    public required decimal ReservoirCapacityUnits { get; init; }

    /// <summary>
    /// Approximate units of insulin consumed during a pod/site fill (priming
    /// the cannula and tubing). Reduces the deliverable units available
    /// before the next site change.
    /// </summary>
    public required decimal PrimingUnits { get; init; }
}
