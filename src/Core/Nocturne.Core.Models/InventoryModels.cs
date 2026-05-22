using System.Text.Json.Serialization;

namespace Nocturne.Core.Models;

/// <summary>
/// Top-level diabetes supply inventory category.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InventoryCategory>))]
public enum InventoryCategory
{
    Cgm,
    Pump,
    Insulin,
    Testing,
    Emergency,
    Other
}

/// <summary>
/// Specific inventory item kind used for default catalog and auto-consumption.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InventoryKind>))]
public enum InventoryKind
{
    CgmSensor,
    CgmTransmitter,
    Pod,
    InfusionSet,
    Cannula,
    Reservoir,
    PumpBattery,
    Insulin,
    TestStrip,
    Lancet,
    AlcoholSwab,
    ControlSolution,
    Glucagon,
    FastCarbs,
    KetoneStrip,
    Custom
}

/// <summary>
/// Source family that can automatically consume inventory.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InventoryAutoConsumeSource>))]
public enum InventoryAutoConsumeSource
{
    None,
    DeviceEvent,
    Bolus,
    BasalInjection
}

/// <summary>
/// Therapy mode picker for seeding the inventory catalog. Drives whether
/// insulin auto-consumes via Bolus/BasalInjection (MDI) or via the linked
/// Pod/Reservoir DeviceEvent (Pump). T1D patients use one or the other —
/// there is no concurrent both-modes therapy, so the enum has no Mixed value.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<TherapyMode>))]
public enum TherapyMode
{
    Mdi,
    Pump
}

/// <summary>
/// Storage/use state for inventory batches.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InventoryStorageState>))]
public enum InventoryStorageState
{
    Normal,
    Refrigerated,
    Opened,
    Frozen,
    HeatExposed,
    Discarded
}

/// <summary>
/// Immutable inventory ledger transaction type.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<InventoryTransactionType>))]
public enum InventoryTransactionType
{
    Restock,
    ManualConsume,
    AutoConsume,
    Adjustment,
    Reversal,
    Expired
}
