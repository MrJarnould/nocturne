using Nocturne.Core.Models;
using Nocturne.Infrastructure.Data.Entities;

namespace Nocturne.Infrastructure.Data.Abstractions;

/// <summary>
/// Repository port for diabetes supply inventory items, batches, and ledger transactions.
/// Tenant isolation is enforced via RLS on every query.
/// </summary>
public interface IInventoryRepository
{
    Task<List<InventoryItemEntity>> GetItemsAsync(bool includeArchived = false, CancellationToken ct = default);
    Task<InventoryItemEntity?> GetItemAsync(Guid itemId, CancellationToken ct = default);
    Task<List<InventoryBatchEntity>> GetBatchesAsync(Guid itemId, CancellationToken ct = default);
    Task<List<InventoryTransactionEntity>> GetTransactionsAsync(Guid itemId, int limit = 100, CancellationToken ct = default);

    /// <summary>
    /// Bulk-load every consume transaction (ManualConsume + AutoConsume) across
    /// the current tenant since the given timestamp. Used to compute
    /// historical-rate run-out projections in one query rather than N.
    /// </summary>
    Task<List<InventoryTransactionEntity>> GetRecentConsumeTransactionsAsync(DateTime since, CancellationToken ct = default);
    Task<InventoryItemEntity> CreateItemAsync(InventoryItemEntity item, CancellationToken ct = default);
    Task<InventoryItemEntity?> UpdateItemAsync(InventoryItemEntity item, CancellationToken ct = default);
    Task<InventoryItemEntity?> ArchiveItemAsync(Guid itemId, CancellationToken ct = default);
    Task<InventoryBatchEntity> AddBatchAsync(InventoryBatchEntity batch, CancellationToken ct = default);
    Task<InventoryBatchEntity?> UpdateBatchAsync(InventoryBatchEntity batch, CancellationToken ct = default);
    Task<InventoryTransactionEntity> AddTransactionAsync(InventoryTransactionEntity transaction, CancellationToken ct = default);
    Task<List<InventoryTransactionEntity>> GetSourceTransactionsAsync(string sourceType, string sourceId, CancellationToken ct = default);
    Task<bool> HasSourceTransactionAsync(Guid itemId, string sourceType, string sourceId, CancellationToken ct = default);
    Task<InventoryItemEntity?> FindAutoConsumeItemAsync(
        InventoryKind kind,
        InventoryAutoConsumeSource source,
        Guid? patientInsulinId,
        CancellationToken ct = default);
    Task<List<InventoryItemEntity>> FindDeviceEventItemsAsync(string eventType, CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
