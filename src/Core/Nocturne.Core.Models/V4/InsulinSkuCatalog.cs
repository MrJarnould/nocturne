namespace Nocturne.Core.Models.V4;

/// <summary>
/// Static catalog of <see cref="InsulinSku"/>s — physical pack-size variants
/// of <see cref="InsulinFormulation"/>s. One formulation typically ships in
/// multiple pack forms (vial, cartridge, disposable pen); inventory tracks
/// stock against the SKU, while dose-math reads pharmacokinetic properties
/// from the formulation.
/// </summary>
/// <remarks>
/// Phase 0 seeds the most common commercial pack form for each catalogued
/// formulation. Additional pack forms can be appended without breaking
/// historical inventory references — SKU IDs are stable and never reused.
/// </remarks>
public static class InsulinSkuCatalog
{
    private static readonly IReadOnlyList<InsulinSku> _skus =
    [
        // ── Rapid-acting U100 (standard) ───────────────────────────────
        new() { Id = "humalog-vial-10ml",       FormulationId = "humalog",   Form = InsulinPackForm.Vial,         UnitsPerPack = 1000, Name = "Humalog 10 mL vial" },
        new() { Id = "humalog-cartridge-3ml",   FormulationId = "humalog",   Form = InsulinPackForm.Cartridge,    UnitsPerPack = 300,  Name = "Humalog 3 mL cartridge" },
        new() { Id = "humalog-kwikpen-3ml",     FormulationId = "humalog",   Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "Humalog 3 mL KwikPen" },
        new() { Id = "admelog-vial-10ml",       FormulationId = "admelog",   Form = InsulinPackForm.Vial,         UnitsPerPack = 1000, Name = "Admelog 10 mL vial" },
        new() { Id = "novorapid-vial-10ml",     FormulationId = "novorapid", Form = InsulinPackForm.Vial,         UnitsPerPack = 1000, Name = "NovoRapid 10 mL vial" },
        new() { Id = "novorapid-penfill-3ml",   FormulationId = "novorapid", Form = InsulinPackForm.Cartridge,    UnitsPerPack = 300,  Name = "NovoRapid 3 mL Penfill" },
        new() { Id = "novorapid-flexpen-3ml",   FormulationId = "novorapid", Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "NovoRapid 3 mL FlexPen" },
        new() { Id = "apidra-vial-10ml",        FormulationId = "apidra",    Form = InsulinPackForm.Vial,         UnitsPerPack = 1000, Name = "Apidra 10 mL vial" },
        new() { Id = "apidra-solostar-3ml",     FormulationId = "apidra",    Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "Apidra 3 mL SoloStar" },
        new() { Id = "fiasp-vial-10ml",         FormulationId = "fiasp",     Form = InsulinPackForm.Vial,         UnitsPerPack = 1000, Name = "Fiasp 10 mL vial" },
        new() { Id = "fiasp-penfill-3ml",       FormulationId = "fiasp",     Form = InsulinPackForm.Cartridge,    UnitsPerPack = 300,  Name = "Fiasp 3 mL Penfill" },
        new() { Id = "fiasp-flextouch-3ml",     FormulationId = "fiasp",     Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "Fiasp 3 mL FlexTouch" },
        new() { Id = "lyumjev-vial-10ml",       FormulationId = "lyumjev",   Form = InsulinPackForm.Vial,         UnitsPerPack = 1000, Name = "Lyumjev 10 mL vial" },
        new() { Id = "lyumjev-kwikpen-3ml",     FormulationId = "lyumjev",   Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "Lyumjev 3 mL KwikPen" },

        // ── Rapid-acting U200 ──────────────────────────────────────────
        new() { Id = "humalog-u200-kwikpen-3ml", FormulationId = "humalog-u200", Form = InsulinPackForm.DisposablePen, UnitsPerPack = 600, Name = "Humalog U200 3 mL KwikPen" },
        new() { Id = "lyumjev-u200-kwikpen-3ml", FormulationId = "lyumjev-u200", Form = InsulinPackForm.DisposablePen, UnitsPerPack = 600, Name = "Lyumjev U200 3 mL KwikPen" },

        // ── Short-acting ───────────────────────────────────────────────
        new() { Id = "humulin-r-vial-10ml",      FormulationId = "humulin-r",      Form = InsulinPackForm.Vial, UnitsPerPack = 1000,  Name = "Humulin R 10 mL vial" },
        new() { Id = "humulin-r-u500-vial-20ml", FormulationId = "humulin-r-u500", Form = InsulinPackForm.Vial, UnitsPerPack = 10000, Name = "Humulin R U500 20 mL vial" },
        new() { Id = "actrapid-vial-10ml",       FormulationId = "actrapid",       Form = InsulinPackForm.Vial, UnitsPerPack = 1000,  Name = "Actrapid 10 mL vial" },

        // ── Long-acting ────────────────────────────────────────────────
        new() { Id = "lantus-vial-10ml",     FormulationId = "lantus",  Form = InsulinPackForm.Vial,         UnitsPerPack = 1000, Name = "Lantus 10 mL vial" },
        new() { Id = "lantus-solostar-3ml",  FormulationId = "lantus",  Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "Lantus 3 mL SoloStar" },
        new() { Id = "levemir-flexpen-3ml",  FormulationId = "levemir", Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "Levemir 3 mL FlexPen" },

        // ── Ultra-long-acting ──────────────────────────────────────────
        new() { Id = "tresiba-flextouch-3ml",      FormulationId = "tresiba",      Form = InsulinPackForm.DisposablePen, UnitsPerPack = 300, Name = "Tresiba 3 mL FlexTouch" },
        new() { Id = "tresiba-u200-flextouch-3ml", FormulationId = "tresiba-u200", Form = InsulinPackForm.DisposablePen, UnitsPerPack = 600, Name = "Tresiba U200 3 mL FlexTouch" },
        new() { Id = "toujeo-solostar-1.5ml",      FormulationId = "toujeo",       Form = InsulinPackForm.DisposablePen, UnitsPerPack = 450, Name = "Toujeo 1.5 mL SoloStar" },

        // No SKUs for "custom", diluted formulations (u10/u40/u50), or pump-shop
        // dilutions — those are compounded ad-hoc and don't ship as a SKU.
    ];

    /// <summary>Returns all known insulin SKUs.</summary>
    public static IReadOnlyList<InsulinSku> GetAll() => _skus;

    /// <summary>Looks up an insulin SKU by its unique identifier.</summary>
    public static InsulinSku? GetById(string id) =>
        _skus.FirstOrDefault(s => s.Id == id);

    /// <summary>
    /// Returns every SKU that ships the given <see cref="InsulinFormulation"/>.
    /// </summary>
    public static IReadOnlyList<InsulinSku> GetForFormulation(string formulationId) =>
        _skus.Where(s => s.FormulationId == formulationId).ToList();
}
