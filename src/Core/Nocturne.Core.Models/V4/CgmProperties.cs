namespace Nocturne.Core.Models.V4;

/// <summary>
/// CGM-specific properties for a device catalog entry.
/// </summary>
public record CgmProperties
{
    public required int SensorDurationDays { get; init; }
    public required int WarmupMinutes { get; init; }
    public required int UpdateIntervalMinutes { get; init; }
    public required bool HasSeparateTransmitter { get; init; }
    public int? TransmitterDurationDays { get; init; }
}
