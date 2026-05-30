using Nocturne.Core.Models;
using Nocturne.Core.Models.Inventory;
using Nocturne.Core.Models.V4;

namespace Nocturne.Core.Contracts.Inventory;

/// <summary>
/// Service for diabetes supply inventory stock calculations, batch movements, and source-based auto-consumption.
/// Tenant isolation is enforced by RLS — methods do not take a userId parameter.
/// </summary>
public interface IInventoryService
{
    Task<IReadOnlyList<InventoryItemDto>> GetItemsAsync(bool includeArchived = false, CancellationToken ct = default);
    Task<InventoryItemDetailDto?> GetItemAsync(Guid itemId, CancellationToken ct = default);

    /// <summary>
    /// Returns the device catalog filtered to the given therapy mode.
    /// The frontend uses this to populate the multi-step seed wizard.
    /// </summary>
    IReadOnlyList<InventoryCatalogEntry> GetInventoryCatalog(TherapyMode mode);

    /// <summary>
    /// Materializes inventory items based on the user's catalog selection plus
    /// the generic supply defaults (strips, lancets, swabs, etc.). Idempotent
    /// per (kind, name): re-running adds nothing for already-seeded items.
    /// </summary>
    Task<IReadOnlyList<InventoryItemDto>> SeedFromSelectionAsync(InventorySeedRequest request, CancellationToken ct = default);

    Task<InventoryItemDto> CreateItemAsync(InventoryItemRequest request, CancellationToken ct = default);
    Task<InventoryItemDto?> UpdateItemAsync(Guid itemId, InventoryItemRequest request, CancellationToken ct = default);
    Task<bool> ArchiveItemAsync(Guid itemId, CancellationToken ct = default);
    Task<InventoryBatchDto?> AddBatchAsync(Guid itemId, InventoryBatchRequest request, CancellationToken ct = default);
    Task<InventoryBatchDto?> UpdateBatchMetadataAsync(Guid batchId, InventoryBatchMetadataRequest request, CancellationToken ct = default);
    Task<InventoryItemDetailDto?> ConsumeAsync(Guid itemId, InventoryConsumeRequest request, CancellationToken ct = default);
    Task<InventoryItemDetailDto?> AdjustBatchAsync(Guid batchId, InventoryAdjustBatchRequest request, CancellationToken ct = default);
    Task<InventoryBatchDto?> TransferBatchToExpiredAsync(Guid batchId, string? notes, CancellationToken ct = default);
    Task AutoConsumeForDeviceEventAsync(DeviceEvent deviceEvent, CancellationToken ct = default);
    Task AutoConsumeForBolusAsync(Bolus bolus, Guid? requestedPatientInsulinId, CancellationToken ct = default);
    Task AutoConsumeForBasalInjectionAsync(BasalInjection basalInjection, CancellationToken ct = default);
    Task ReverseSourceAsync(string sourceType, string sourceId, CancellationToken ct = default);

    /// <summary>
    /// All transactions across the tenant, optionally filtered by type.
    /// Used by the global History view.
    /// </summary>
    Task<IReadOnlyList<InventoryTransactionWithItemDto>> GetAllTransactionsAsync(
        InventoryTransactionType? type = null,
        DateTime? since = null,
        int limit = 200,
        CancellationToken ct = default);

    /// <summary>
    /// Batches across the tenant expiring within the given threshold.
    /// </summary>
    Task<IReadOnlyList<InventoryExpiringBatchDto>> GetExpiringBatchesAsync(int thresholdDays = 30, CancellationToken ct = default);
}

/// <summary>
/// User's selection from the device-catalog wizard.
/// </summary>
public record InventorySeedRequest(
    TherapyMode TherapyMode,
    string[] CgmKeys,
    string? PumpKey,
    string? RapidInsulinKey,
    string? BasalInsulinKey);

public record InventoryItemRequest(
    string Name,
    InventoryCategory Category,
    InventoryKind Kind,
    string UnitLabel,
    decimal LowStockThreshold,
    decimal? TargetStock,
    bool AutoConsumeEnabled,
    InventoryAutoConsumeSource AutoConsumeSource,
    Guid? PatientInsulinId,
    string[]? DeviceEventTypes,
    Guid? LinkedInsulinItemId = null,
    decimal? LinkedInsulinUnitsPerUse = null,
    int? WearDays = null);

public record InventoryBatchRequest(
    decimal Quantity,
    DateTime? ReceivedAt,
    DateTime? ExpiresAt,
    string? LotNumber,
    InventoryStorageState StorageState,
    string? Notes);

public record InventoryBatchMetadataRequest(
    DateTime? ReceivedAt,
    DateTime? ExpiresAt,
    string? LotNumber,
    InventoryStorageState StorageState,
    string? Notes);

public record InventoryConsumeRequest(decimal Quantity, string? Reason, Guid? BatchId, string? Notes);

public record InventoryAdjustBatchRequest(decimal RemainingQuantity, string? Reason, string? Notes);

public class InventoryItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public InventoryCategory Category { get; set; }
    public InventoryKind Kind { get; set; }
    public string UnitLabel { get; set; } = "each";
    public decimal LowStockThreshold { get; set; }
    public decimal? TargetStock { get; set; }
    public bool AutoConsumeEnabled { get; set; }
    public InventoryAutoConsumeSource AutoConsumeSource { get; set; }
    public Guid? PatientInsulinId { get; set; }
    public string[] DeviceEventTypes { get; set; } = [];
    public Guid? LinkedInsulinItemId { get; set; }
    public decimal? LinkedInsulinUnitsPerUse { get; set; }
    public int? WearDays { get; set; }
    public bool IsDefault { get; set; }
    public bool IsArchived { get; set; }
    public decimal TotalStock { get; set; }
    public decimal UsableStock { get; set; }
    public decimal ExpiredStock { get; set; }
    public decimal ExpiringSoonStock { get; set; }
    public DateTime? LowestExpiry { get; set; }
    public bool IsLow { get; set; }
    public decimal SuggestedRestockQuantity { get; set; }

    /// <summary>
    /// Projected date the item's usable stock will reach zero, based on
    /// either wear-time (for items with <see cref="WearDays"/>),
    /// historical consumption rate, or a linked pod/reservoir item.
    /// Null when no projection is possible (no wear-time and no history).
    /// </summary>
    public DateTime? EstimatedRunOutAt { get; set; }

    /// <summary>
    /// Which projection strategy produced <see cref="EstimatedRunOutAt"/>.
    /// Useful for UI tooltips so users understand where the date comes from.
    /// </summary>
    public RunOutProjectionSource? RunOutProjectionSource { get; set; }
}

/// <summary>
/// How an item's run-out date was projected. Surfaced on the DTO so the UI
/// can render different tooltips ("based on 10-day wear time" vs "based on
/// average use over the last 14 days").
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter<RunOutProjectionSource>))]
public enum RunOutProjectionSource
{
    /// <summary>Computed from wear_days × usable_stock (sensors, pods, etc.).</summary>
    WearTime,
    /// <summary>Computed from average daily consumption over the recent ledger window.</summary>
    HistoricalRate,
    /// <summary>Computed from a linked pod/reservoir's wear-time (pump-mode insulin).</summary>
    LinkedItem
}

public class InventoryItemDetailDto : InventoryItemDto
{
    public List<InventoryBatchDto> Batches { get; set; } = [];
    public List<InventoryTransactionDto> Transactions { get; set; } = [];
}

public class InventoryBatchDto
{
    public Guid Id { get; set; }
    public Guid InventoryItemId { get; set; }
    public decimal ReceivedQuantity { get; set; }
    public decimal RemainingQuantity { get; set; }
    public DateTime ReceivedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string? LotNumber { get; set; }
    public InventoryStorageState StorageState { get; set; }
    public string? Notes { get; set; }
    public bool IsExpired { get; set; }
    public bool IsUsable { get; set; }
}

public class InventoryTransactionDto
{
    public Guid Id { get; set; }
    public Guid InventoryItemId { get; set; }
    public Guid? InventoryBatchId { get; set; }
    public InventoryTransactionType Type { get; set; }
    public decimal QuantityDelta { get; set; }
    public decimal QuantityAfter { get; set; }
    public string? Reason { get; set; }
    public string? SourceType { get; set; }
    public string? SourceId { get; set; }
    public DateTime? SourceTimestamp { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Transaction DTO enriched with the parent item's name — used in the global History view.
/// </summary>
public class InventoryTransactionWithItemDto : InventoryTransactionDto
{
    public string ItemName { get; set; } = string.Empty;
}

/// <summary>
/// A batch that is expiring soon, enriched with its parent item's name and id.
/// Used in the global History / upcoming-expirations view.
/// </summary>
public class InventoryExpiringBatchDto : InventoryBatchDto
{
    public string ItemName { get; set; } = string.Empty;
}
