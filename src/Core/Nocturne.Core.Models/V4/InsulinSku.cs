namespace Nocturne.Core.Models.V4;

/// <summary>
/// A SKU (stocking unit) of a specific <see cref="InsulinFormulation"/> in a
/// particular pack form and pack size. Inventory items reference SKUs;
/// pharmacokinetic math (DIA, peak, curve, concentration) lives on the
/// formulation. The split keeps formulation invariants stable while letting
/// the catalog grow as new pack forms come to market.
/// </summary>
/// <remarks>
/// Like <see cref="DeviceCatalogEntry"/> and <see cref="ConsumableCatalogEntry"/>,
/// SKUs are append-only. A manufacturer changing pack size mid-product must
/// result in a new <see cref="Id"/>.
/// </remarks>
/// <seealso cref="InsulinSkuCatalog"/>
/// <seealso cref="InsulinFormulation"/>
/// <seealso cref="PatientInsulin"/>
public record InsulinSku
{
    /// <summary>
    /// Unique kebab-case identifier
    /// (e.g., "humalog-vial-10ml", "fiasp-cartridge-3ml").
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// FK to the parent <see cref="InsulinFormulation.Id"/>. Auto-consume
    /// from a Bolus / BasalInjection drains FEFO across all SKUs of the
    /// active <see cref="PatientInsulin"/>'s formulation.
    /// </summary>
    public required string FormulationId { get; init; }

    /// <summary>
    /// Physical pack form (vial / cartridge / disposable pen).
    /// </summary>
    public required InsulinPackForm Form { get; init; }

    /// <summary>
    /// Total units of insulin per individual pack
    /// (e.g. 1000 for a 10 mL U100 vial, 300 for a 3 mL U100 cartridge,
    /// 600 for a 3 mL U200 cartridge).
    /// </summary>
    public required decimal UnitsPerPack { get; init; }

    /// <summary>
    /// Human-readable display name
    /// (e.g., "Humalog 10 mL vial", "Fiasp 3 mL cartridge").
    /// </summary>
    public required string Name { get; init; }
}
