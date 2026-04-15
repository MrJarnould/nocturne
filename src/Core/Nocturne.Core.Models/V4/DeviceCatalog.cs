namespace Nocturne.Core.Models.V4;

/// <summary>
/// Static catalog of known CGM and insulin pump device models.
/// </summary>
public static class DeviceCatalog
{
    private static readonly IReadOnlyList<DeviceCatalogEntry> _entries =
    [
        // CGMs — Dexcom
        new() { Id = "dexcom-g7",       Name = "Dexcom G7",       Manufacturer = "Dexcom",    Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 10, WarmupMinutes = 30,  UpdateIntervalMinutes = 5, HasSeparateTransmitter = false } },
        new() { Id = "dexcom-g6",       Name = "Dexcom G6",       Manufacturer = "Dexcom",    Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 10, WarmupMinutes = 120, UpdateIntervalMinutes = 5, HasSeparateTransmitter = true, TransmitterDurationDays = 90 } },
        new() { Id = "dexcom-one-plus", Name = "Dexcom ONE+",     Manufacturer = "Dexcom",    Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 10, WarmupMinutes = 30,  UpdateIntervalMinutes = 5, HasSeparateTransmitter = false } },
        new() { Id = "dexcom-one",      Name = "Dexcom ONE",      Manufacturer = "Dexcom",    Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 10, WarmupMinutes = 120, UpdateIntervalMinutes = 5, HasSeparateTransmitter = true, TransmitterDurationDays = 90 } },

        // CGMs — Abbott
        new() { Id = "libre-3",         Name = "FreeStyle Libre 3",  Manufacturer = "Abbott", Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 14, WarmupMinutes = 60, UpdateIntervalMinutes = 1, HasSeparateTransmitter = false } },
        new() { Id = "libre-2-plus",    Name = "FreeStyle Libre 2+", Manufacturer = "Abbott", Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 15, WarmupMinutes = 60, UpdateIntervalMinutes = 1, HasSeparateTransmitter = false } },
        new() { Id = "libre-2",         Name = "FreeStyle Libre 2",  Manufacturer = "Abbott", Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 14, WarmupMinutes = 60, UpdateIntervalMinutes = 1, HasSeparateTransmitter = false } },

        // CGMs — Medtronic
        new() { Id = "medtronic-guardian-4", Name = "Guardian 4", Manufacturer = "Medtronic", Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 7, WarmupMinutes = 120, UpdateIntervalMinutes = 5, HasSeparateTransmitter = false } },
        new() { Id = "medtronic-guardian-3", Name = "Guardian 3", Manufacturer = "Medtronic", Category = DeviceCategory.CGM, Cgm = new() { SensorDurationDays = 7, WarmupMinutes = 120, UpdateIntervalMinutes = 5, HasSeparateTransmitter = true, TransmitterDurationDays = 365 } },

        // CGMs — Custom
        new() { Id = "custom-cgm", Name = "Custom CGM", Manufacturer = "Custom", Category = DeviceCategory.CGM },

        // Pumps
        new() { Id = "omnipod-5",        Name = "Omnipod 5",     Manufacturer = "Insulet",    Category = DeviceCategory.InsulinPump },
        new() { Id = "omnipod-dash",     Name = "Omnipod DASH",  Manufacturer = "Insulet",    Category = DeviceCategory.InsulinPump },
        new() { Id = "tandem-tslim-x2",  Name = "t:slim X2",    Manufacturer = "Tandem",     Category = DeviceCategory.InsulinPump },
        new() { Id = "tandem-mobi",      Name = "t:slim Mobi",   Manufacturer = "Tandem",     Category = DeviceCategory.InsulinPump },
        new() { Id = "medtronic-780g",   Name = "MiniMed 780G",  Manufacturer = "Medtronic",  Category = DeviceCategory.InsulinPump },
        new() { Id = "medtronic-770g",   Name = "MiniMed 770G",  Manufacturer = "Medtronic",  Category = DeviceCategory.InsulinPump },
        new() { Id = "ypsopump",         Name = "YpsoPump",      Manufacturer = "Ypsomed",    Category = DeviceCategory.InsulinPump },
        new() { Id = "dana-i",           Name = "Dana-i",        Manufacturer = "SOOIL",      Category = DeviceCategory.InsulinPump },
        new() { Id = "dana-rs",          Name = "Dana RS",       Manufacturer = "SOOIL",      Category = DeviceCategory.InsulinPump },
        new() { Id = "custom-pump",      Name = "Custom Pump",   Manufacturer = "Custom",     Category = DeviceCategory.InsulinPump },
    ];

    public static IReadOnlyList<DeviceCatalogEntry> GetAll() => _entries;

    public static DeviceCatalogEntry? GetById(string id) =>
        _entries.FirstOrDefault(e => e.Id == id);

    public static IReadOnlyList<DeviceCatalogEntry> GetByCategory(DeviceCategory category) =>
        _entries.Where(e => e.Category == category).ToList();
}
