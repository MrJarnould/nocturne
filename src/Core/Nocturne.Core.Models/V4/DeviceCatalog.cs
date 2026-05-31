namespace Nocturne.Core.Models.V4;

/// <summary>
/// Static catalog of known device models. Each entry composes optional
/// capability records (<see cref="CgmCapability"/>, <see cref="PumpCapability"/>,
/// <see cref="AidCapability"/>) describing what the device actually does;
/// <see cref="DeviceCatalogEntry.PrimaryCategory"/> is for UI grouping only.
/// Consumables for each device are catalogued separately in
/// <see cref="ConsumableCatalog"/>.
/// </summary>
public static class DeviceCatalog
{
    private static readonly IReadOnlyList<DeviceCatalogEntry> _entries =
    [
        // ── CGMs — Dexcom ──────────────────────────────────────────────
        new()
        {
            Id = "dexcom-g7",
            Name = "Dexcom G7",
            Manufacturer = "Dexcom",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 10,
                WarmupMinutes = 30,
                UpdateIntervalMinutes = 5,
                HasSeparateTransmitter = false,
                TransmitterIsRechargeable = false,
            },
        },
        new()
        {
            Id = "dexcom-g6",
            Name = "Dexcom G6",
            Manufacturer = "Dexcom",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 10,
                WarmupMinutes = 120,
                UpdateIntervalMinutes = 5,
                HasSeparateTransmitter = true,
                TransmitterIsRechargeable = false,
                TransmitterDurationDays = 90,
            },
        },
        new()
        {
            Id = "dexcom-one-plus",
            Name = "Dexcom ONE+",
            Manufacturer = "Dexcom",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 10,
                WarmupMinutes = 30,
                UpdateIntervalMinutes = 5,
                HasSeparateTransmitter = false,
                TransmitterIsRechargeable = false,
            },
        },
        new()
        {
            Id = "dexcom-one",
            Name = "Dexcom ONE",
            Manufacturer = "Dexcom",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 10,
                WarmupMinutes = 120,
                UpdateIntervalMinutes = 5,
                HasSeparateTransmitter = true,
                TransmitterIsRechargeable = false,
                TransmitterDurationDays = 90,
            },
        },

        // ── CGMs — Abbott ──────────────────────────────────────────────
        new()
        {
            Id = "libre-3",
            Name = "FreeStyle Libre 3",
            Manufacturer = "Abbott",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 14,
                WarmupMinutes = 60,
                UpdateIntervalMinutes = 1,
                HasSeparateTransmitter = false,
                TransmitterIsRechargeable = false,
            },
        },
        new()
        {
            Id = "libre-2-plus",
            Name = "FreeStyle Libre 2+",
            Manufacturer = "Abbott",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 15,
                WarmupMinutes = 60,
                UpdateIntervalMinutes = 1,
                HasSeparateTransmitter = false,
                TransmitterIsRechargeable = false,
            },
        },
        new()
        {
            Id = "libre-2",
            Name = "FreeStyle Libre 2",
            Manufacturer = "Abbott",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 14,
                WarmupMinutes = 60,
                UpdateIntervalMinutes = 1,
                HasSeparateTransmitter = false,
                TransmitterIsRechargeable = false,
            },
        },

        // ── CGMs — Medtronic ───────────────────────────────────────────
        new()
        {
            Id = "medtronic-guardian-4",
            Name = "Guardian 4",
            Manufacturer = "Medtronic",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 7,
                WarmupMinutes = 120,
                UpdateIntervalMinutes = 5,
                HasSeparateTransmitter = false,
                TransmitterIsRechargeable = false,
            },
        },
        new()
        {
            // Guardian 3: rechargeable transmitter, sensor-only consumable.
            Id = "medtronic-guardian-3",
            Name = "Guardian 3",
            Manufacturer = "Medtronic",
            PrimaryCategory = DeviceCategory.CGM,
            Cgm = new()
            {
                SensorDurationDays = 7,
                WarmupMinutes = 120,
                UpdateIntervalMinutes = 5,
                HasSeparateTransmitter = true,
                TransmitterIsRechargeable = true,
                TransmitterDurationDays = 365,
            },
        },

        // ── CGMs — Custom ──────────────────────────────────────────────
        new()
        {
            Id = "custom-cgm",
            Name = "Custom CGM",
            Manufacturer = "Custom",
            PrimaryCategory = DeviceCategory.CGM,
        },

        // ── Pumps ──────────────────────────────────────────────────────
        new()
        {
            Id = "omnipod-5",
            Name = "Omnipod 5",
            Manufacturer = "Insulet",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = true, ReservoirCapacityUnits = 200, PrimingUnits = 13 },
            Aid = new() { Algorithm = AidAlgorithm.Omnipod5Algorithm, IsCommercial = true },
        },
        new()
        {
            Id = "omnipod-dash",
            Name = "Omnipod DASH",
            Manufacturer = "Insulet",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = true, ReservoirCapacityUnits = 200, PrimingUnits = 13 },
        },
        new()
        {
            Id = "tandem-tslim-x2",
            Name = "t:slim X2",
            Manufacturer = "Tandem",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = false, ReservoirCapacityUnits = 300, PrimingUnits = 10 },
            Aid = new() { Algorithm = AidAlgorithm.ControlIQ, IsCommercial = true },
        },
        new()
        {
            Id = "tandem-mobi",
            Name = "t:slim Mobi",
            Manufacturer = "Tandem",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = false, ReservoirCapacityUnits = 200, PrimingUnits = 10 },
            Aid = new() { Algorithm = AidAlgorithm.ControlIQ, IsCommercial = true },
        },
        new()
        {
            Id = "medtronic-780g",
            Name = "MiniMed 780G",
            Manufacturer = "Medtronic",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = false, ReservoirCapacityUnits = 300, PrimingUnits = 10 },
            Aid = new() { Algorithm = AidAlgorithm.MedtronicSmartGuard, IsCommercial = true },
        },
        new()
        {
            Id = "medtronic-770g",
            Name = "MiniMed 770G",
            Manufacturer = "Medtronic",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = false, ReservoirCapacityUnits = 300, PrimingUnits = 10 },
            Aid = new() { Algorithm = AidAlgorithm.MedtronicSmartGuard, IsCommercial = true },
        },
        new()
        {
            Id = "ypsopump",
            Name = "YpsoPump",
            Manufacturer = "Ypsomed",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = false, ReservoirCapacityUnits = 160, PrimingUnits = 10 },
        },
        new()
        {
            Id = "dana-i",
            Name = "Dana-i",
            Manufacturer = "SOOIL",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = false, ReservoirCapacityUnits = 300, PrimingUnits = 10 },
        },
        new()
        {
            Id = "dana-rs",
            Name = "Dana RS",
            Manufacturer = "SOOIL",
            PrimaryCategory = DeviceCategory.InsulinPump,
            Pump = new() { IsTubeless = false, ReservoirCapacityUnits = 300, PrimingUnits = 10 },
        },
        new()
        {
            // Custom pump — no specs known.
            Id = "custom-pump",
            Name = "Custom Pump",
            Manufacturer = "Custom",
            PrimaryCategory = DeviceCategory.InsulinPump,
        },
    ];

    /// <summary>
    /// Returns all known device catalog entries across all categories.
    /// </summary>
    public static IReadOnlyList<DeviceCatalogEntry> GetAll() => _entries;

    /// <summary>
    /// Looks up a device catalog entry by its unique identifier.
    /// </summary>
    public static DeviceCatalogEntry? GetById(string id) =>
        _entries.FirstOrDefault(e => e.Id == id);

    /// <summary>
    /// Returns all catalog entries whose
    /// <see cref="DeviceCatalogEntry.PrimaryCategory"/> matches the argument.
    /// </summary>
    public static IReadOnlyList<DeviceCatalogEntry> GetByCategory(DeviceCategory category) =>
        _entries.Where(e => e.PrimaryCategory == category).ToList();
}
