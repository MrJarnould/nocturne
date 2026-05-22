using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Nocturne.Core.Models;
using Nocturne.Infrastructure.Data.Abstractions;
using Nocturne.Infrastructure.Data.Entities;

namespace Nocturne.Infrastructure.Data.Repositories;

/// <summary>
/// EF Core repository for diabetes supply inventory.
/// Tenant isolation is enforced by RLS — queries never need a tenant filter.
/// </summary>
public class InventoryRepository : IInventoryRepository
{
    private readonly NocturneDbContext _context;

    public InventoryRepository(NocturneDbContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryItemEntity>> GetItemsAsync(
        bool includeArchived = false,
        CancellationToken ct = default)
    {
        var query = _context.InventoryItems
            .Include(i => i.Batches)
            .AsNoTracking()
            .AsQueryable();

        if (!includeArchived)
            query = query.Where(i => !i.IsArchived);

        return await query
            .OrderBy(i => i.Category)
            .ThenBy(i => i.Name)
            .ToListAsync(ct);
    }

    public async Task<InventoryItemEntity?> GetItemAsync(Guid itemId, CancellationToken ct = default)
    {
        return await _context.InventoryItems
            .Include(i => i.Batches)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == itemId, ct);
    }

    public async Task<List<InventoryBatchEntity>> GetBatchesAsync(Guid itemId, CancellationToken ct = default)
    {
        return await _context.InventoryBatches
            .AsNoTracking()
            .Where(b => b.InventoryItemId == itemId)
            .OrderBy(b => b.ExpiresAt == null)
            .ThenBy(b => b.ExpiresAt)
            .ThenBy(b => b.ReceivedAt)
            .ThenBy(b => b.Id)
            .ToListAsync(ct);
    }

    public async Task<List<InventoryTransactionEntity>> GetTransactionsAsync(
        Guid itemId,
        int limit = 100,
        CancellationToken ct = default)
    {
        return await _context.InventoryTransactions
            .AsNoTracking()
            .Include(t => t.Batch)
            .Where(t => t.InventoryItemId == itemId)
            .OrderByDescending(t => t.CreatedAt)
            .Take(limit)
            .ToListAsync(ct);
    }

    public async Task<List<InventoryTransactionEntity>> GetRecentConsumeTransactionsAsync(
        DateTime since,
        CancellationToken ct = default)
    {
        return await _context.InventoryTransactions
            .AsNoTracking()
            .Where(t => t.CreatedAt >= since
                && (t.Type == InventoryTransactionType.ManualConsume
                    || t.Type == InventoryTransactionType.AutoConsume))
            .ToListAsync(ct);
    }

    public async Task<InventoryItemEntity> CreateItemAsync(InventoryItemEntity item, CancellationToken ct = default)
    {
        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync(ct);
        return item;
    }

    public async Task<InventoryItemEntity?> UpdateItemAsync(InventoryItemEntity item, CancellationToken ct = default)
    {
        var existing = await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id == item.Id, ct);
        if (existing is null)
            return null;

        existing.Name = item.Name;
        existing.Category = item.Category;
        existing.Kind = item.Kind;
        existing.UnitLabel = item.UnitLabel;
        existing.LowStockThreshold = item.LowStockThreshold;
        existing.TargetStock = item.TargetStock;
        existing.AutoConsumeEnabled = item.AutoConsumeEnabled;
        existing.AutoConsumeSource = item.AutoConsumeSource;
        existing.PatientInsulinId = item.PatientInsulinId;
        existing.DeviceEventTypesJson = item.DeviceEventTypesJson;
        existing.LinkedInsulinItemId = item.LinkedInsulinItemId;
        existing.LinkedInsulinUnitsPerUse = item.LinkedInsulinUnitsPerUse;
        existing.WearDays = item.WearDays;
        existing.IsArchived = item.IsArchived;

        await _context.SaveChangesAsync(ct);
        return existing;
    }

    public async Task<InventoryItemEntity?> ArchiveItemAsync(Guid itemId, CancellationToken ct = default)
    {
        var existing = await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id == itemId, ct);
        if (existing is null)
            return null;

        existing.IsArchived = true;
        await _context.SaveChangesAsync(ct);
        return existing;
    }

    public async Task<InventoryBatchEntity> AddBatchAsync(InventoryBatchEntity batch, CancellationToken ct = default)
    {
        _context.InventoryBatches.Add(batch);
        await _context.SaveChangesAsync(ct);
        return batch;
    }

    public async Task<InventoryBatchEntity?> UpdateBatchAsync(
        InventoryBatchEntity batch,
        CancellationToken ct = default)
    {
        var existing = await _context.InventoryBatches
            .Include(b => b.Item)
            .FirstOrDefaultAsync(b => b.Id == batch.Id, ct);
        if (existing is null)
            return null;

        existing.RemainingQuantity = batch.RemainingQuantity;
        existing.ReceivedQuantity = batch.ReceivedQuantity;
        existing.ReceivedAt = batch.ReceivedAt;
        existing.ExpiresAt = batch.ExpiresAt;
        existing.LotNumber = batch.LotNumber;
        existing.StorageState = batch.StorageState;
        existing.Notes = batch.Notes;

        await _context.SaveChangesAsync(ct);
        return existing;
    }

    public async Task<InventoryTransactionEntity> AddTransactionAsync(
        InventoryTransactionEntity transaction,
        CancellationToken ct = default)
    {
        _context.InventoryTransactions.Add(transaction);
        await _context.SaveChangesAsync(ct);
        return transaction;
    }

    public async Task<List<InventoryTransactionEntity>> GetSourceTransactionsAsync(
        string sourceType,
        string sourceId,
        CancellationToken ct = default)
    {
        return await _context.InventoryTransactions
            .Include(t => t.Item)
            .Where(t => t.SourceType == sourceType && t.SourceId == sourceId)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<bool> HasSourceTransactionAsync(
        Guid itemId,
        string sourceType,
        string sourceId,
        CancellationToken ct = default)
    {
        return await _context.InventoryTransactions.AnyAsync(
            t => t.InventoryItemId == itemId
                 && t.SourceType == sourceType
                 && t.SourceId == sourceId
                 && t.Type != InventoryTransactionType.Reversal,
            ct);
    }

    public async Task<InventoryItemEntity?> FindAutoConsumeItemAsync(
        InventoryKind kind,
        InventoryAutoConsumeSource source,
        Guid? patientInsulinId,
        CancellationToken ct = default)
    {
        var query = _context.InventoryItems
            .Include(i => i.Batches)
            .Where(i => !i.IsArchived
                        && i.AutoConsumeEnabled
                        && i.Kind == kind
                        && i.AutoConsumeSource == source);

        if (patientInsulinId.HasValue)
            query = query.Where(i => i.PatientInsulinId == patientInsulinId);

        return await query
            .OrderByDescending(i => i.PatientInsulinId == patientInsulinId)
            .ThenBy(i => i.Name)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<InventoryItemEntity>> FindDeviceEventItemsAsync(
        string eventType,
        CancellationToken ct = default)
    {
        var candidates = await _context.InventoryItems
            .Include(i => i.Batches)
            .Where(i => !i.IsArchived
                        && i.AutoConsumeEnabled
                        && i.AutoConsumeSource == InventoryAutoConsumeSource.DeviceEvent)
            .ToListAsync(ct);

        return candidates
            .Where(i => DeserializeEventTypes(i.DeviceEventTypesJson).Contains(eventType, StringComparer.OrdinalIgnoreCase))
            .ToList();
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);

    private static string[] DeserializeEventTypes(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return [];

        try
        {
            return JsonSerializer.Deserialize<string[]>(json) ?? [];
        }
        catch
        {
            return [];
        }
    }
}
