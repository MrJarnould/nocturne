namespace Nocturne.Core.Models.V4;

/// <summary>
/// CGM capability composed onto a <see cref="DeviceCatalogEntry"/>. Describes
/// sensor lifetime, transmission characteristics, and whether the transmitter
/// is a disposable consumable (e.g. Dexcom G6) or a rechargeable durable (e.g.
/// Eversense). Disposable transmitters get a <see cref="ConsumableCatalogEntry"/>;
/// rechargeable transmitters do not.
/// </summary>
/// <seealso cref="DeviceCatalogEntry"/>
/// <seealso cref="ConsumableCatalogEntry"/>
public record CgmCapability
{
    /// <summary>
    /// Maximum approved wear duration for a single sensor in days
    /// (e.g., 10 for Dexcom G7, 14 for Libre 3, 180/365 for Eversense).
    /// </summary>
    public required int SensorDurationDays { get; init; }

    /// <summary>
    /// Warm-up period in minutes before the CGM begins reporting readings
    /// after sensor insertion.
    /// </summary>
    public required int WarmupMinutes { get; init; }

    /// <summary>
    /// How often the CGM transmits a new glucose reading, in minutes
    /// (e.g., 5 for Dexcom, 1 for Libre 3).
    /// </summary>
    public required int UpdateIntervalMinutes { get; init; }

    /// <summary>
    /// Whether this CGM model uses a separate (non-integrated) transmitter
    /// in addition to the sensor.
    /// </summary>
    public required bool HasSeparateTransmitter { get; init; }

    /// <summary>
    /// Whether the separate transmitter is rechargeable and reused across
    /// sensor insertions (true = Eversense; transmitter is a durable, not a
    /// consumable) or single-use disposable (false = Dexcom G6; transmitter
    /// is a 90-day consumable). Meaningful only when
    /// <see cref="HasSeparateTransmitter"/> is true.
    /// </summary>
    public required bool TransmitterIsRechargeable { get; init; }

    /// <summary>
    /// Maximum lifetime of a disposable separate transmitter in days. Null
    /// when there is no separate transmitter, or when the transmitter is
    /// rechargeable.
    /// </summary>
    public int? TransmitterDurationDays { get; init; }
}
