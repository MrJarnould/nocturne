namespace Nocturne.Core.Models.V4;

/// <summary>
/// Static catalog of consumable SKUs per <see cref="DeviceCatalogEntry"/>.
/// Each row represents something a patient *buys and stocks* (a sensor, a pod,
/// a reservoir, an infusion set); device-level capabilities live on the parent
/// <see cref="DeviceCatalogEntry"/>.
/// </summary>
/// <remarks>
/// <para>
/// <b>Catalog rules captured here (per the Phase 0 architecture):</b>
/// </para>
/// <list type="bullet">
///   <item>
///     <description>
///       Single-piece CGMs (G7, ONE+, Libre 3 / 2+ / 2, Guardian 4) emit
///       exactly one sensor SKU.
///     </description>
///   </item>
///   <item>
///     <description>
///       Two-piece CGMs with <b>disposable</b> transmitters (Dexcom G6, ONE)
///       emit a sensor SKU plus a transmitter SKU with its
///       <see cref="CgmCapability.TransmitterDurationDays"/>.
///     </description>
///   </item>
///   <item>
///     <description>
///       Two-piece CGMs with <b>rechargeable</b> transmitters (Guardian 3,
///       Eversense) emit only a sensor SKU — the transmitter is a durable on
///       <see cref="PatientDevice"/>, not a consumable.
///     </description>
///   </item>
///   <item>
///     <description>
///       Tubeless pumps (Omnipod 5 / DASH) emit one pod SKU with
///       <see cref="CannulaSpec"/>; the pod integrates the cannula.
///     </description>
///   </item>
///   <item>
///     <description>
///       Tubed pumps emit a reservoir SKU and an infusion-set SKU. A separate
///       cannula SKU is emitted only when the manufacturer actually sells
///       cannulas independently of the set body (Tandem yes, Medtronic no).
///     </description>
///   </item>
/// </list>
/// </remarks>
public static class ConsumableCatalog
{
    private static readonly IReadOnlyList<ConsumableCatalogEntry> _entries =
    [
        // ── CGM sensors (single-piece) ─────────────────────────────────
        new()
        {
            Id = "dexcom-g7-sensor",
            DeviceCatalogId = "dexcom-g7",
            Kind = ConsumableKind.CgmSensor,
            Name = "Dexcom G7 sensor",
            UnitLabel = "sensors",
            WearDays = 10,
        },
        new()
        {
            Id = "dexcom-one-plus-sensor",
            DeviceCatalogId = "dexcom-one-plus",
            Kind = ConsumableKind.CgmSensor,
            Name = "Dexcom ONE+ sensor",
            UnitLabel = "sensors",
            WearDays = 10,
        },
        new()
        {
            Id = "libre-3-sensor",
            DeviceCatalogId = "libre-3",
            Kind = ConsumableKind.CgmSensor,
            Name = "FreeStyle Libre 3 sensor",
            UnitLabel = "sensors",
            WearDays = 14,
        },
        new()
        {
            Id = "libre-2-plus-sensor",
            DeviceCatalogId = "libre-2-plus",
            Kind = ConsumableKind.CgmSensor,
            Name = "FreeStyle Libre 2+ sensor",
            UnitLabel = "sensors",
            WearDays = 15,
        },
        new()
        {
            Id = "libre-2-sensor",
            DeviceCatalogId = "libre-2",
            Kind = ConsumableKind.CgmSensor,
            Name = "FreeStyle Libre 2 sensor",
            UnitLabel = "sensors",
            WearDays = 14,
        },
        new()
        {
            Id = "medtronic-guardian-4-sensor",
            DeviceCatalogId = "medtronic-guardian-4",
            Kind = ConsumableKind.CgmSensor,
            Name = "Guardian 4 sensor",
            UnitLabel = "sensors",
            WearDays = 7,
        },

        // ── CGM sensors with rechargeable transmitters ─────────────────
        // Transmitter itself is a durable; only the sensor is a consumable.
        new()
        {
            Id = "medtronic-guardian-3-sensor",
            DeviceCatalogId = "medtronic-guardian-3",
            Kind = ConsumableKind.CgmSensor,
            Name = "Guardian 3 sensor",
            UnitLabel = "sensors",
            WearDays = 7,
        },

        // ── CGMs with disposable transmitters ──────────────────────────
        new()
        {
            Id = "dexcom-g6-sensor",
            DeviceCatalogId = "dexcom-g6",
            Kind = ConsumableKind.CgmSensor,
            Name = "Dexcom G6 sensor",
            UnitLabel = "sensors",
            WearDays = 10,
        },
        new()
        {
            Id = "dexcom-g6-transmitter",
            DeviceCatalogId = "dexcom-g6",
            Kind = ConsumableKind.CgmTransmitter,
            Name = "Dexcom G6 transmitter",
            UnitLabel = "transmitters",
            WearDays = 90,
        },
        new()
        {
            Id = "dexcom-one-sensor",
            DeviceCatalogId = "dexcom-one",
            Kind = ConsumableKind.CgmSensor,
            Name = "Dexcom ONE sensor",
            UnitLabel = "sensors",
            WearDays = 10,
        },
        new()
        {
            Id = "dexcom-one-transmitter",
            DeviceCatalogId = "dexcom-one",
            Kind = ConsumableKind.CgmTransmitter,
            Name = "Dexcom ONE transmitter",
            UnitLabel = "transmitters",
            WearDays = 90,
        },

        // ── Pods (tubeless pumps) ──────────────────────────────────────
        new()
        {
            Id = "omnipod-5-pod",
            DeviceCatalogId = "omnipod-5",
            Kind = ConsumableKind.Pod,
            Name = "Omnipod 5 pod",
            UnitLabel = "pods",
            WearDays = 3,
            Cannula = new() { Gauge = 25, LengthMm = 8m, Material = "Teflon" },
        },
        new()
        {
            Id = "omnipod-dash-pod",
            DeviceCatalogId = "omnipod-dash",
            Kind = ConsumableKind.Pod,
            Name = "Omnipod DASH pod",
            UnitLabel = "pods",
            WearDays = 3,
            Cannula = new() { Gauge = 25, LengthMm = 8m, Material = "Teflon" },
        },

        // ── Tubed pumps — Tandem (reservoir + set + separate cannula) ──
        // Tandem sells the AutoSoft 90 infusion set body and the cannula
        // separately, so a standalone Cannula SKU is appropriate.
        new()
        {
            Id = "tandem-tslim-x2-reservoir",
            DeviceCatalogId = "tandem-tslim-x2",
            Kind = ConsumableKind.Reservoir,
            Name = "t:slim X2 cartridge",
            UnitLabel = "cartridges",
        },
        new()
        {
            Id = "tandem-tslim-x2-infusion-set",
            DeviceCatalogId = "tandem-tslim-x2",
            Kind = ConsumableKind.InfusionSet,
            Name = "t:slim X2 infusion set",
            UnitLabel = "sets",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },
        new()
        {
            Id = "tandem-tslim-x2-cannula",
            DeviceCatalogId = "tandem-tslim-x2",
            Kind = ConsumableKind.Cannula,
            Name = "t:slim X2 cannula",
            UnitLabel = "cannulas",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },
        new()
        {
            Id = "tandem-mobi-reservoir",
            DeviceCatalogId = "tandem-mobi",
            Kind = ConsumableKind.Reservoir,
            Name = "t:slim Mobi cartridge",
            UnitLabel = "cartridges",
        },
        new()
        {
            Id = "tandem-mobi-infusion-set",
            DeviceCatalogId = "tandem-mobi",
            Kind = ConsumableKind.InfusionSet,
            Name = "t:slim Mobi infusion set",
            UnitLabel = "sets",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },
        new()
        {
            Id = "tandem-mobi-cannula",
            DeviceCatalogId = "tandem-mobi",
            Kind = ConsumableKind.Cannula,
            Name = "t:slim Mobi cannula",
            UnitLabel = "cannulas",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },

        // ── Tubed pumps — Medtronic (reservoir + integrated set, no
        //    separate cannula SKU) ────────────────────────────────────────
        new()
        {
            Id = "medtronic-780g-reservoir",
            DeviceCatalogId = "medtronic-780g",
            Kind = ConsumableKind.Reservoir,
            Name = "MiniMed 780G reservoir",
            UnitLabel = "reservoirs",
        },
        new()
        {
            Id = "medtronic-780g-infusion-set",
            DeviceCatalogId = "medtronic-780g",
            Kind = ConsumableKind.InfusionSet,
            Name = "MiniMed 780G infusion set",
            UnitLabel = "sets",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },
        new()
        {
            Id = "medtronic-770g-reservoir",
            DeviceCatalogId = "medtronic-770g",
            Kind = ConsumableKind.Reservoir,
            Name = "MiniMed 770G reservoir",
            UnitLabel = "reservoirs",
        },
        new()
        {
            Id = "medtronic-770g-infusion-set",
            DeviceCatalogId = "medtronic-770g",
            Kind = ConsumableKind.InfusionSet,
            Name = "MiniMed 770G infusion set",
            UnitLabel = "sets",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },

        // ── Tubed pumps — Ypsomed / SOOIL ──────────────────────────────
        new()
        {
            Id = "ypsopump-reservoir",
            DeviceCatalogId = "ypsopump",
            Kind = ConsumableKind.Reservoir,
            Name = "YpsoPump cartridge",
            UnitLabel = "cartridges",
        },
        new()
        {
            Id = "ypsopump-infusion-set",
            DeviceCatalogId = "ypsopump",
            Kind = ConsumableKind.InfusionSet,
            Name = "YpsoPump Orbit infusion set",
            UnitLabel = "sets",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },
        new()
        {
            Id = "dana-i-reservoir",
            DeviceCatalogId = "dana-i",
            Kind = ConsumableKind.Reservoir,
            Name = "Dana-i cartridge",
            UnitLabel = "cartridges",
        },
        new()
        {
            Id = "dana-i-infusion-set",
            DeviceCatalogId = "dana-i",
            Kind = ConsumableKind.InfusionSet,
            Name = "Dana-i infusion set",
            UnitLabel = "sets",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },
        new()
        {
            Id = "dana-rs-reservoir",
            DeviceCatalogId = "dana-rs",
            Kind = ConsumableKind.Reservoir,
            Name = "Dana RS cartridge",
            UnitLabel = "cartridges",
        },
        new()
        {
            Id = "dana-rs-infusion-set",
            DeviceCatalogId = "dana-rs",
            Kind = ConsumableKind.InfusionSet,
            Name = "Dana RS infusion set",
            UnitLabel = "sets",
            WearDays = 3,
            Cannula = new() { Gauge = 27, LengthMm = 9m, Material = "Teflon" },
        },
    ];

    /// <summary>Returns all known consumable SKUs across all devices.</summary>
    public static IReadOnlyList<ConsumableCatalogEntry> GetAll() => _entries;

    /// <summary>Looks up a consumable SKU by its unique identifier.</summary>
    public static ConsumableCatalogEntry? GetById(string id) =>
        _entries.FirstOrDefault(e => e.Id == id);

    /// <summary>
    /// Returns every consumable SKU that belongs to the given device model
    /// (FK on <see cref="ConsumableCatalogEntry.DeviceCatalogId"/>).
    /// </summary>
    public static IReadOnlyList<ConsumableCatalogEntry> GetForDevice(string deviceCatalogId) =>
        _entries.Where(e => e.DeviceCatalogId == deviceCatalogId).ToList();

    /// <summary>
    /// Returns every consumable SKU of the given <see cref="ConsumableKind"/>
    /// across all devices.
    /// </summary>
    public static IReadOnlyList<ConsumableCatalogEntry> GetByKind(ConsumableKind kind) =>
        _entries.Where(e => e.Kind == kind).ToList();
}
