using System.Text.Json.Serialization;

namespace Nocturne.Core.Models.Inventory;

/// <summary>
/// Top-level categories the device-selection wizard groups by.
/// Generic supplies (test strips, lancets, etc.) are not in the catalog —
/// they're seeded unconditionally because brand granularity adds no value
/// for stock tracking on those items.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InventoryCatalogCategory>))]
public enum InventoryCatalogCategory
{
    Cgm,
    Pump,
    RapidInsulin,
    BasalInsulin
}

/// <summary>
/// One pickable product in the device catalog. A catalog entry expands into
/// one or more inventory items when seeded — e.g. <c>dexcom-g6</c> seeds both
/// a sensor item and a transmitter item.
/// </summary>
public record InventoryCatalogEntry(
    string Key,
    InventoryCatalogCategory Category,
    string Brand,
    string Name,
    InventoryCatalogItemSpec[] Items,
    string? Notes = null,
    bool IsOtc = false,
    bool IsDiscontinued = false,
    DateTime? DiscontinuationDate = null);

/// <summary>
/// A single inventory item to materialize when a catalog entry is seeded.
/// </summary>
public record InventoryCatalogItemSpec(
    InventoryKind Kind,
    string Name,
    string UnitLabel,
    decimal LowStockThreshold,
    decimal? TargetStock,
    InventoryAutoConsumeSource AutoConsumeSource,
    string[] DeviceEventTypes,
    InventoryCategory InventoryCategory,
    int? WearDays = null,
    decimal? LinkedInsulinUnitsPerUse = null);

/// <summary>
/// Static device catalog. Curated list of CGM products, insulin pumps and
/// insulin brands commonly used by T1D patients. Updated by editing this
/// file and recompiling — there is no DB-backed source-of-truth.
/// </summary>
public static class InventoryCatalog
{
    /// <summary>Returns all catalog entries relevant to the given therapy mode.</summary>
    public static IReadOnlyList<InventoryCatalogEntry> ForMode(TherapyMode mode) => mode switch
    {
        // Pump users still log basal injections only if they take long-acting alongside the pump,
        // which isn't the standard regimen — so pump mode hides basal insulin.
        TherapyMode.Pump => [..Cgms, ..Pumps, ..RapidInsulins],
        TherapyMode.Mdi => [..Cgms, ..RapidInsulins, ..BasalInsulins],
        _ => []
    };

    public static InventoryCatalogEntry? FindByKey(string key)
        => All.FirstOrDefault(e => string.Equals(e.Key, key, StringComparison.OrdinalIgnoreCase));

    public static IReadOnlyList<InventoryCatalogEntry> All => [..Cgms, ..Pumps, ..RapidInsulins, ..BasalInsulins];

    // ── CGM catalog ────────────────────────────────────────────────────

    public static readonly InventoryCatalogEntry[] Cgms =
    [
        new("dexcom-g7", InventoryCatalogCategory.Cgm, "Dexcom", "Dexcom G7",
            Items: [SensorItem("Dexcom G7 sensors", wearDays: 10)],
            Notes: "Real-time alerts; main prescription CGM."),
        new("dexcom-g7-15", InventoryCatalogCategory.Cgm, "Dexcom", "Dexcom G7 15 Day",
            Items: [SensorItem("Dexcom G7 15 Day sensors", wearDays: 15)],
            Notes: "Adult-focused longer-wear G7 (15.5-day with grace period)."),
        new("dexcom-g6", InventoryCatalogCategory.Cgm, "Dexcom", "Dexcom G6",
            Items:
            [
                SensorItem("Dexcom G6 sensors", wearDays: 10),
                TransmitterItem("Dexcom G6 transmitters", wearDays: 90)
            ],
            Notes: "Being phased out in US from July 1, 2026 — sensor + separate 90-day transmitter."),
        new("dexcom-stelo", InventoryCatalogCategory.Cgm, "Dexcom", "Stelo",
            Items: [SensorItem("Stelo sensors", wearDays: 15)],
            IsOtc: true,
            Notes: "OTC biosensor for adults not using insulin."),
        new("libre-2", InventoryCatalogCategory.Cgm, "Abbott", "FreeStyle Libre 2",
            Items: [SensorItem("FreeStyle Libre 2 sensors", wearDays: 14)],
            Notes: "Widely used Libre generation; availability varies by region."),
        new("libre-3", InventoryCatalogCategory.Cgm, "Abbott", "FreeStyle Libre 3",
            Items: [SensorItem("FreeStyle Libre 3 sensors", wearDays: 14)],
            Notes: "Smaller real-time Libre CGM."),
        new("libre-3-plus", InventoryCatalogCategory.Cgm, "Abbott", "FreeStyle Libre 3 Plus",
            Items: [SensorItem("FreeStyle Libre 3 Plus sensors", wearDays: 15)],
            Notes: "15-day Libre sensor with real-time readings."),
        new("libre-rio", InventoryCatalogCategory.Cgm, "Abbott", "Libre Rio",
            Items: [SensorItem("Libre Rio sensors", wearDays: 14)],
            IsOtc: true,
            Notes: "OTC CGM for adults with type 2 diabetes not using insulin."),
        new("lingo", InventoryCatalogCategory.Cgm, "Abbott", "Lingo",
            Items: [SensorItem("Lingo sensors", wearDays: 14)],
            IsOtc: true,
            Notes: "OTC wellness biosensor; not for diabetes management."),
        new("guardian-4", InventoryCatalogCategory.Cgm, "Medtronic", "Guardian 4",
            Items: [SensorItem("Guardian 4 sensors", wearDays: 7)],
            Notes: "MiniMed pump ecosystem (esp. 780G)."),
        new("simplera-sync", InventoryCatalogCategory.Cgm, "Medtronic", "Simplera Sync",
            Items: [SensorItem("Simplera Sync sensors", wearDays: 7)],
            Notes: "All-in-one for MiniMed 780G; approved 2025."),
        new("instinct", InventoryCatalogCategory.Cgm, "Medtronic", "Instinct sensor",
            Items: [SensorItem("Instinct sensors", wearDays: 7)],
            Notes: "Integrated sensor listed for MiniMed 780G compatibility."),
        new("eversense-e3", InventoryCatalogCategory.Cgm, "Senseonics", "Eversense E3",
            Items: [SensorItem("Eversense E3 sensors", wearDays: 180, lowStockThreshold: 1, targetStock: 1)],
            Notes: "Implantable; up to 180 days."),
        new("eversense-365", InventoryCatalogCategory.Cgm, "Senseonics", "Eversense 365",
            Items: [SensorItem("Eversense 365 sensors", wearDays: 365, lowStockThreshold: 1, targetStock: 1)],
            Notes: "Implantable; up to one year. FDA-cleared 2024.")
    ];

    // ── Pump catalog ───────────────────────────────────────────────────

    public static readonly InventoryCatalogEntry[] Pumps =
    [
        new("omnipod-5", InventoryCatalogCategory.Pump, "Insulet", "Omnipod 5",
            Items: [PodItem("Omnipod 5 pods", reservoirCapacity: 200, eventTypes: ["PodChange", "PodActivated"])],
            Notes: "Tubeless pod with integrated cannula. 200 u capacity."),
        new("omnipod-dash", InventoryCatalogCategory.Pump, "Insulet", "Omnipod DASH",
            Items: [PodItem("Omnipod DASH pods", reservoirCapacity: 200, eventTypes: ["PodChange", "PodActivated"])],
            Notes: "Tubeless pod (predecessor to Omnipod 5). 200 u capacity."),
        new("tslim-x2", InventoryCatalogCategory.Pump, "Tandem", "t:slim X2",
            Items:
            [
                ReservoirItem("Tandem cartridges", reservoirCapacity: 300, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("Tandem infusion sets"),
                CannulaItem("Tandem cannulas")
            ],
            Notes: "Tubed pump; 300 u cartridge, separate infusion sets and cannulas."),
        new("mobi", InventoryCatalogCategory.Pump, "Tandem", "Tandem Mobi",
            Items:
            [
                ReservoirItem("Tandem Mobi cartridges", reservoirCapacity: 200, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("Mobi infusion sets"),
                CannulaItem("Mobi cannulas")
            ],
            Notes: "Compact tubed pump; 200 u cartridge."),
        new("780g", InventoryCatalogCategory.Pump, "Medtronic", "MiniMed 780G",
            Items:
            [
                ReservoirItem("Medtronic reservoirs", reservoirCapacity: 300, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("Medtronic infusion sets"),
                CannulaItem("Medtronic cannulas")
            ],
            Notes: "Tubed; 300 u reservoir."),
        new("770g", InventoryCatalogCategory.Pump, "Medtronic", "MiniMed 770G",
            Items:
            [
                ReservoirItem("Medtronic reservoirs", reservoirCapacity: 300, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("Medtronic infusion sets"),
                CannulaItem("Medtronic cannulas")
            ],
            Notes: "Tubed; 300 u reservoir."),
        new("ilet-180", InventoryCatalogCategory.Pump, "Beta Bionics", "iLet (1.6 mL cartridge)",
            Items:
            [
                ReservoirItem("iLet cartridges", reservoirCapacity: 180, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("iLet infusion sets"),
                CannulaItem("iLet cannulas")
            ],
            Notes: "Bionic pancreas; 1.6 mL / 180 u cartridge variant."),
        new("ilet-300", InventoryCatalogCategory.Pump, "Beta Bionics", "iLet (3.0 mL cartridge)",
            Items:
            [
                ReservoirItem("iLet cartridges", reservoirCapacity: 300, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("iLet infusion sets"),
                CannulaItem("iLet cannulas")
            ],
            Notes: "Bionic pancreas; 3.0 mL / 300 u cartridge variant."),
        new("ypsopump", InventoryCatalogCategory.Pump, "Ypsomed", "YpsoPump",
            Items:
            [
                ReservoirItem("YpsoPump cartridges", reservoirCapacity: 160, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("Orbit infusion sets"),
                CannulaItem("Orbit cannulas")
            ],
            Notes: "Tubed; 160 u cartridge. Common in Europe."),
        new("twiist", InventoryCatalogCategory.Pump, "Sequel", "twiist",
            Items:
            [
                ReservoirItem("twiist cartridges", reservoirCapacity: 300, eventTypes: ["ReservoirChange", "InsulinChange"]),
                InfusionSetItem("twiist infusion sets"),
                CannulaItem("twiist cannulas")
            ],
            Notes: "Tubed; 300 u cartridge; AAA-battery powered.")
    ];

    // ── Insulin catalog ────────────────────────────────────────────────

    public static readonly InventoryCatalogEntry[] RapidInsulins =
    [
        new("humalog", InventoryCatalogCategory.RapidInsulin, "Eli Lilly", "Humalog",
            Items: [InsulinItem("Humalog (Lispro)", auto: InventoryAutoConsumeSource.Bolus)],
            Notes: "Lispro."),
        new("lyumjev", InventoryCatalogCategory.RapidInsulin, "Eli Lilly", "Lyumjev",
            Items: [InsulinItem("Lyumjev (Ultra-rapid Lispro)", auto: InventoryAutoConsumeSource.Bolus)],
            Notes: "Ultra-rapid Lispro."),
        new("novolog", InventoryCatalogCategory.RapidInsulin, "Novo Nordisk", "NovoLog / NovoRapid",
            Items: [InsulinItem("NovoLog (Aspart)", auto: InventoryAutoConsumeSource.Bolus)],
            Notes: "Aspart. Marketed as NovoLog in the US; NovoRapid elsewhere."),
        new("fiasp", InventoryCatalogCategory.RapidInsulin, "Novo Nordisk", "Fiasp",
            Items: [InsulinItem("Fiasp (Faster Aspart)", auto: InventoryAutoConsumeSource.Bolus)],
            Notes: "Faster Aspart."),
        new("apidra", InventoryCatalogCategory.RapidInsulin, "Sanofi", "Apidra",
            Items: [InsulinItem("Apidra (Glulisine)", auto: InventoryAutoConsumeSource.Bolus)],
            Notes: "Glulisine."),
        new("admelog", InventoryCatalogCategory.RapidInsulin, "Sanofi", "Admelog",
            Items: [InsulinItem("Admelog (biosimilar Lispro)", auto: InventoryAutoConsumeSource.Bolus)],
            Notes: "Biosimilar Lispro."),
        new("trurapi", InventoryCatalogCategory.RapidInsulin, "Sanofi", "Trurapi",
            Items: [InsulinItem("Trurapi (biosimilar Aspart)", auto: InventoryAutoConsumeSource.Bolus)],
            Notes: "Biosimilar Aspart.")
    ];

    public static readonly InventoryCatalogEntry[] BasalInsulins =
    [
        new("tresiba", InventoryCatalogCategory.BasalInsulin, "Novo Nordisk", "Tresiba",
            Items: [InsulinItem("Tresiba (Degludec)", auto: InventoryAutoConsumeSource.BasalInjection)],
            Notes: "Degludec."),
        new("lantus", InventoryCatalogCategory.BasalInsulin, "Sanofi", "Lantus",
            Items: [InsulinItem("Lantus (Glargine U100)", auto: InventoryAutoConsumeSource.BasalInjection)],
            Notes: "Glargine U100."),
        new("toujeo", InventoryCatalogCategory.BasalInsulin, "Sanofi", "Toujeo",
            Items: [InsulinItem("Toujeo (Glargine U300)", auto: InventoryAutoConsumeSource.BasalInjection)],
            Notes: "Glargine U300."),
        new("basaglar", InventoryCatalogCategory.BasalInsulin, "Eli Lilly", "Basaglar",
            Items: [InsulinItem("Basaglar (biosimilar Glargine)", auto: InventoryAutoConsumeSource.BasalInjection)],
            Notes: "Biosimilar Glargine."),
        new("levemir", InventoryCatalogCategory.BasalInsulin, "Novo Nordisk", "Levemir",
            Items: [InsulinItem("Levemir (Detemir)", auto: InventoryAutoConsumeSource.BasalInjection)],
            Notes: "Detemir. Discontinued in some markets."),
        new("semglee", InventoryCatalogCategory.BasalInsulin, "Mylan", "Semglee",
            Items: [InsulinItem("Semglee (biosimilar Glargine)", auto: InventoryAutoConsumeSource.BasalInjection)],
            Notes: "Biosimilar Glargine.")
    ];

    // ── Item-spec helpers ──────────────────────────────────────────────

    private static InventoryCatalogItemSpec SensorItem(string name, int wearDays, decimal lowStockThreshold = 2, decimal? targetStock = 4)
        => new(
            Kind: InventoryKind.CgmSensor,
            Name: name,
            UnitLabel: "sensors",
            LowStockThreshold: lowStockThreshold,
            TargetStock: targetStock,
            AutoConsumeSource: InventoryAutoConsumeSource.DeviceEvent,
            // Only "SensorStart" — AAPS emits both Sensor Start AND Sensor
            // Change/Stop for the same physical session boundary, and consuming
            // on both would double-count. Trio emits only Sensor Start, so this
            // covers both uploaders correctly. Manual UI clicks ("Change sensor")
            // also produce SensorChange events — those bypass the catalog match
            // intentionally so users can wastage-log without affecting stock.
            DeviceEventTypes: ["SensorStart"],
            InventoryCategory: InventoryCategory.Cgm,
            WearDays: wearDays);

    private static InventoryCatalogItemSpec TransmitterItem(string name, int wearDays)
        => new(
            Kind: InventoryKind.CgmTransmitter,
            Name: name,
            UnitLabel: "transmitters",
            LowStockThreshold: 1,
            TargetStock: 1,
            AutoConsumeSource: InventoryAutoConsumeSource.None,
            DeviceEventTypes: [],
            InventoryCategory: InventoryCategory.Cgm,
            WearDays: wearDays);

    private static InventoryCatalogItemSpec PodItem(string name, decimal reservoirCapacity, string[] eventTypes)
        => new(
            Kind: InventoryKind.Pod,
            Name: name,
            UnitLabel: "pods",
            LowStockThreshold: 2,
            TargetStock: 5,
            AutoConsumeSource: InventoryAutoConsumeSource.DeviceEvent,
            // Trio (the dominant pump uploader) emits "Site Change" for Omnipod
            // activations, not "Pod Change". Subscribe to both so manual UI
            // clicks (PodChange) and Trio uploads (SiteChange) both match.
            DeviceEventTypes: [..eventTypes, "SiteChange"],
            InventoryCategory: InventoryCategory.Pump,
            WearDays: 3,
            LinkedInsulinUnitsPerUse: reservoirCapacity);

    private static InventoryCatalogItemSpec ReservoirItem(string name, decimal reservoirCapacity, string[] eventTypes)
        => new(
            Kind: InventoryKind.Reservoir,
            Name: name,
            UnitLabel: "cartridges",
            LowStockThreshold: 2,
            TargetStock: 5,
            AutoConsumeSource: InventoryAutoConsumeSource.DeviceEvent,
            // Trio emits "Site Change" for Medtronic prime events (reservoir
            // refill). Subscribe to both ReservoirChange and SiteChange.
            DeviceEventTypes: [..eventTypes, "SiteChange"],
            InventoryCategory: InventoryCategory.Pump,
            WearDays: 3,
            LinkedInsulinUnitsPerUse: reservoirCapacity);

    private static InventoryCatalogItemSpec InfusionSetItem(string name)
        => new(
            Kind: InventoryKind.InfusionSet,
            Name: name,
            UnitLabel: "sets",
            LowStockThreshold: 2,
            TargetStock: 5,
            AutoConsumeSource: InventoryAutoConsumeSource.DeviceEvent,
            DeviceEventTypes: ["SiteChange"],
            InventoryCategory: InventoryCategory.Pump,
            WearDays: 3);

    private static InventoryCatalogItemSpec CannulaItem(string name)
        => new(
            Kind: InventoryKind.Cannula,
            Name: name,
            UnitLabel: "cannulas",
            LowStockThreshold: 2,
            TargetStock: 5,
            AutoConsumeSource: InventoryAutoConsumeSource.DeviceEvent,
            DeviceEventTypes: ["CannulaChange"],
            InventoryCategory: InventoryCategory.Pump,
            WearDays: 3);

    private static InventoryCatalogItemSpec InsulinItem(string name, InventoryAutoConsumeSource auto)
        => new(
            Kind: InventoryKind.Insulin,
            Name: name,
            UnitLabel: "units",
            LowStockThreshold: 200,
            TargetStock: 600,
            // Pump-mode rapid insulin gets AutoConsumeSource overridden to None at seed time
            // because the bottle drains via the linked Pod/Reservoir change, not via Bolus.
            AutoConsumeSource: auto,
            DeviceEventTypes: [],
            InventoryCategory: InventoryCategory.Insulin);
}
