namespace Nocturne.Core.Models.Configuration;

/// <summary>
/// Configuration for the diabetes-supply inventory subsystem.
/// Bound from the <c>Inventory</c> section in appsettings.
/// </summary>
public class InventoryOptions
{
    public const string SectionName = "Inventory";

    /// <summary>
    /// Number of days before a batch's <c>ExpiresAt</c> at which the
    /// <c>inventory.expiring_soon</c> notification fires. Default 30.
    /// </summary>
    public int ExpirySoonThresholdDays { get; set; } = 30;
}
