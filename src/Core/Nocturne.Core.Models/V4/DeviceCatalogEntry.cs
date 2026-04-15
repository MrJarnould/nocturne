namespace Nocturne.Core.Models.V4;

/// <summary>
/// A known device model with default properties.
/// </summary>
public record DeviceCatalogEntry
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Manufacturer { get; init; }
    public required DeviceCategory Category { get; init; }
    public CgmProperties? Cgm { get; init; }
}
