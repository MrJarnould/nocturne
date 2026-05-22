using System.Text.Json;
using Microsoft.Extensions.Options;
using Nocturne.Core.Contracts.Inventory;
using Nocturne.Core.Contracts.Multitenancy;
using Nocturne.Core.Contracts.Notifications;
using Nocturne.Core.Contracts.V4.Repositories;
using Nocturne.Core.Models;
using Nocturne.Core.Models.Configuration;
using Nocturne.Core.Models.Inventory;
using Nocturne.Core.Models.V4;
using Nocturne.Infrastructure.Data.Abstractions;
using Nocturne.Infrastructure.Data.Entities;

namespace Nocturne.API.Services.Inventory;

/// <summary>
/// Backend-owned diabetes supply inventory service. Tenant-wide:
/// notifications target the tenant owner subject; queries rely on RLS for
/// tenant isolation rather than per-user filters.
/// </summary>
public class InventoryService : IInventoryService
{
    private const string LowStockNotificationType = "inventory.low_stock";
    private const string ExpiringSoonNotificationType = "inventory.expiring_soon";
    private const int HistoricalConsumeWindowDays = 14;
    private const int MinConsumeTransactionsForProjection = 3;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IInventoryRepository _repository;
    private readonly IInAppNotificationRepository _notificationRepository;
    private readonly IInAppNotificationService _notifications;
    private readonly IPatientInsulinRepository _patientInsulins;
    private readonly ITenantOwnerResolver _tenantOwnerResolver;
    private readonly InventoryOptions _options;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(
        IInventoryRepository repository,
        IInAppNotificationRepository notificationRepository,
        IInAppNotificationService notifications,
        IPatientInsulinRepository patientInsulins,
        ITenantOwnerResolver tenantOwnerResolver,
        IOptions<InventoryOptions> options,
        ILogger<InventoryService> logger)
    {
        _repository = repository;
        _notificationRepository = notificationRepository;
        _notifications = notifications;
        _patientInsulins = patientInsulins;
        _tenantOwnerResolver = tenantOwnerResolver;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyList<InventoryItemDto>> GetItemsAsync(
        bool includeArchived = false,
        CancellationToken ct = default)
    {
        var items = await _repository.GetItemsAsync(includeArchived, ct);
        var lookupById = items.ToDictionary(i => i.Id);
        var consumeWindowStart = DateTime.UtcNow.AddDays(-HistoricalConsumeWindowDays);
        var recentConsumes = await _repository.GetRecentConsumeTransactionsAsync(consumeWindowStart, ct)
            ?? new List<InventoryTransactionEntity>();
        var consumesByItem = recentConsumes
            .GroupBy(t => t.InventoryItemId)
            .ToDictionary(g => g.Key, g => (IReadOnlyList<InventoryTransactionEntity>)g.ToList());

        return items
            .Select(i => ToItemDto(i, _options.ExpirySoonThresholdDays, lookupById, consumesByItem))
            .ToArray();
    }

    public async Task<InventoryItemDetailDto?> GetItemAsync(Guid itemId, CancellationToken ct = default)
    {
        var item = await _repository.GetItemAsync(itemId, ct);
        if (item is null)
            return null;

        var transactions = await _repository.GetTransactionsAsync(itemId, 100, ct) ?? new List<InventoryTransactionEntity>();

        // Look up linked Pod/Reservoir item if this is pump-mode insulin.
        InventoryItemEntity? linkedItem = null;
        if (item.Kind == InventoryKind.Insulin)
        {
            // Insulin is on the receiving end of the link; find a Pod/Reservoir
            // item that points at this insulin.
            var allItems = await _repository.GetItemsAsync(includeArchived: false, ct);
            linkedItem = allItems?.FirstOrDefault(i => i.LinkedInsulinItemId == item.Id);
        }

        var lookupById = new Dictionary<Guid, InventoryItemEntity>();
        if (linkedItem is not null) lookupById[linkedItem.Id] = linkedItem;
        lookupById[item.Id] = item;

        var consumes = transactions
            .Where(t => t.Type == InventoryTransactionType.ManualConsume
                     || t.Type == InventoryTransactionType.AutoConsume)
            .Where(t => t.CreatedAt >= DateTime.UtcNow.AddDays(-HistoricalConsumeWindowDays))
            .ToList();
        var consumesByItem = new Dictionary<Guid, IReadOnlyList<InventoryTransactionEntity>>
        {
            [item.Id] = consumes
        };

        return ToDetailDto(item, transactions, _options.ExpirySoonThresholdDays, lookupById, consumesByItem);
    }

    public IReadOnlyList<InventoryCatalogEntry> GetInventoryCatalog(TherapyMode mode)
        => InventoryCatalog.ForMode(mode);

    public async Task<IReadOnlyList<InventoryItemDto>> SeedFromSelectionAsync(InventorySeedRequest request, CancellationToken ct = default)
    {
        var existing = await _repository.GetItemsAsync(includeArchived: true, ct);
        var existingKeys = existing
            .Select(i => $"{i.Kind}:{i.Name}".ToLowerInvariant())
            .ToHashSet();

        // 1. Collect the catalog entries the user picked.
        var picked = new List<InventoryCatalogEntry>();
        foreach (var key in request.CgmKeys ?? [])
        {
            var entry = InventoryCatalog.FindByKey(key);
            if (entry is not null) picked.Add(entry);
        }
        if (!string.IsNullOrEmpty(request.PumpKey))
        {
            var pump = InventoryCatalog.FindByKey(request.PumpKey);
            if (pump is not null) picked.Add(pump);
        }
        if (!string.IsNullOrEmpty(request.RapidInsulinKey))
        {
            var rapid = InventoryCatalog.FindByKey(request.RapidInsulinKey);
            if (rapid is not null) picked.Add(rapid);
        }
        if (!string.IsNullOrEmpty(request.BasalInsulinKey) && request.TherapyMode == TherapyMode.Mdi)
        {
            var basal = InventoryCatalog.FindByKey(request.BasalInsulinKey);
            if (basal is not null) picked.Add(basal);
        }

        // 2. Materialize inventory items from each picked catalog entry's specs.
        //    Pump-mode rapid insulin gets its AutoConsumeSource overridden to None
        //    so the bottle drains via the reservoir/pod link instead of via Bolus.
        foreach (var entry in picked)
        {
            foreach (var spec in entry.Items)
            {
                var key = $"{spec.Kind}:{spec.Name}".ToLowerInvariant();
                if (existingKeys.Contains(key)) continue;
                existingKeys.Add(key);

                var autoConsumeSource = OverrideRapidInsulinSourceForPump(spec, entry, request.TherapyMode);

                await _repository.CreateItemAsync(new InventoryItemEntity
                {
                    Name = spec.Name,
                    Category = spec.InventoryCategory,
                    Kind = spec.Kind,
                    UnitLabel = spec.UnitLabel,
                    LowStockThreshold = spec.LowStockThreshold,
                    TargetStock = spec.TargetStock,
                    AutoConsumeEnabled = autoConsumeSource != InventoryAutoConsumeSource.None
                                         || spec.LinkedInsulinUnitsPerUse.HasValue,
                    AutoConsumeSource = autoConsumeSource,
                    DeviceEventTypesJson = JsonSerializer.Serialize(spec.DeviceEventTypes, JsonOptions),
                    LinkedInsulinUnitsPerUse = spec.LinkedInsulinUnitsPerUse,
                    WearDays = spec.WearDays,
                    IsDefault = true
                }, ct);
            }
        }

        // 3. Seed generic supplies (strips/lancets/swabs/glucagon/etc.) — these
        //    don't have brand granularity and are useful regardless of therapy mode.
        foreach (var spec in GenericSupplyItems)
        {
            var key = $"{spec.Kind}:{spec.Name}".ToLowerInvariant();
            if (existingKeys.Contains(key)) continue;
            existingKeys.Add(key);

            await _repository.CreateItemAsync(new InventoryItemEntity
            {
                Name = spec.Name,
                Category = spec.InventoryCategory,
                Kind = spec.Kind,
                UnitLabel = spec.UnitLabel,
                LowStockThreshold = spec.LowStockThreshold,
                TargetStock = spec.TargetStock,
                AutoConsumeEnabled = false,
                AutoConsumeSource = InventoryAutoConsumeSource.None,
                DeviceEventTypesJson = "[]",
                IsDefault = true
            }, ct);
        }

        // 4. Auto-link Pod/Reservoir items to the rapid insulin item the user picked,
        //    so pump users get a working topology without manual configuration.
        await TryAutoLinkReservoirsToSoleInsulinAsync(ct);

        return await GetItemsAsync(includeArchived: false, ct);
    }

    private static InventoryAutoConsumeSource OverrideRapidInsulinSourceForPump(
        InventoryCatalogItemSpec spec,
        InventoryCatalogEntry entry,
        TherapyMode mode)
    {
        // For pump users, rapid insulin drains via the linked pod/reservoir change
        // (not via Bolus), so override the catalog's Bolus source to None.
        if (mode == TherapyMode.Pump
            && entry.Category == InventoryCatalogCategory.RapidInsulin
            && spec.AutoConsumeSource == InventoryAutoConsumeSource.Bolus)
        {
            return InventoryAutoConsumeSource.None;
        }
        return spec.AutoConsumeSource;
    }

    public async Task<InventoryItemDto> CreateItemAsync(InventoryItemRequest request, CancellationToken ct = default)
    {
        ValidateItemRequest(request);
        var created = await _repository.CreateItemAsync(new InventoryItemEntity
        {
            Name = request.Name.Trim(),
            Category = request.Category,
            Kind = request.Kind,
            UnitLabel = NormalizeUnit(request.UnitLabel),
            LowStockThreshold = request.LowStockThreshold,
            TargetStock = request.TargetStock,
            AutoConsumeEnabled = request.AutoConsumeEnabled,
            AutoConsumeSource = request.AutoConsumeSource,
            PatientInsulinId = request.PatientInsulinId,
            DeviceEventTypesJson = JsonSerializer.Serialize(request.DeviceEventTypes ?? [], JsonOptions),
            LinkedInsulinItemId = request.LinkedInsulinItemId,
            LinkedInsulinUnitsPerUse = request.LinkedInsulinUnitsPerUse,
            WearDays = request.WearDays
        }, ct);

        await EvaluateLowStockAsync(created, ct);
        await EvaluateExpiringSoonAsync(created, ct);
        return ToItemDto(created, _options.ExpirySoonThresholdDays);
    }

    public async Task<InventoryItemDto?> UpdateItemAsync(Guid itemId, InventoryItemRequest request, CancellationToken ct = default)
    {
        ValidateItemRequest(request);
        var updated = await _repository.UpdateItemAsync(new InventoryItemEntity
        {
            Id = itemId,
            Name = request.Name.Trim(),
            Category = request.Category,
            Kind = request.Kind,
            UnitLabel = NormalizeUnit(request.UnitLabel),
            LowStockThreshold = request.LowStockThreshold,
            TargetStock = request.TargetStock,
            AutoConsumeEnabled = request.AutoConsumeEnabled,
            AutoConsumeSource = request.AutoConsumeSource,
            PatientInsulinId = request.PatientInsulinId,
            DeviceEventTypesJson = JsonSerializer.Serialize(request.DeviceEventTypes ?? [], JsonOptions),
            LinkedInsulinItemId = request.LinkedInsulinItemId,
            LinkedInsulinUnitsPerUse = request.LinkedInsulinUnitsPerUse,
            WearDays = request.WearDays
        }, ct);

        if (updated is null)
            return null;

        await EvaluateLowStockAsync(updated, ct);
        await EvaluateExpiringSoonAsync(updated, ct);
        return ToItemDto(updated, _options.ExpirySoonThresholdDays);
    }

    public async Task<bool> ArchiveItemAsync(Guid itemId, CancellationToken ct = default)
    {
        // Fetch with batches first so we can archive any expiring-soon notifications keyed by batch.
        var pre = await _repository.GetItemAsync(itemId, ct);
        var archived = await _repository.ArchiveItemAsync(itemId, ct);
        if (archived is null)
            return false;

        var ownerId = await GetTenantOwnerSubjectIdAsync(ct);
        if (ownerId is not null)
        {
            await _notifications.ArchiveBySourceAsync(ownerId, LowStockNotificationType, itemId.ToString(), NotificationArchiveReason.Completed, ct);

            if (pre is not null)
            {
                foreach (var batch in pre.Batches)
                {
                    await _notifications.ArchiveBySourceAsync(ownerId, ExpiringSoonNotificationType, batch.Id.ToString(), NotificationArchiveReason.Completed, ct);
                }
            }
        }
        return true;
    }

    public async Task<InventoryBatchDto?> AddBatchAsync(
        Guid itemId,
        InventoryBatchRequest request,
        CancellationToken ct = default)
    {
        if (request.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(request));

        var item = await _repository.GetItemAsync(itemId, ct);
        if (item is null)
            return null;

        var batch = await _repository.AddBatchAsync(new InventoryBatchEntity
        {
            InventoryItemId = itemId,
            ReceivedQuantity = request.Quantity,
            RemainingQuantity = request.Quantity,
            ReceivedAt = NormalizeDate(request.ReceivedAt) ?? DateTime.UtcNow,
            ExpiresAt = NormalizeDate(request.ExpiresAt),
            LotNumber = request.LotNumber,
            StorageState = request.StorageState,
            Notes = request.Notes
        }, ct);

        await _repository.AddTransactionAsync(new InventoryTransactionEntity
        {
            InventoryItemId = itemId,
            InventoryBatchId = batch.Id,
            Type = InventoryTransactionType.Restock,
            QuantityDelta = request.Quantity,
            QuantityAfter = batch.RemainingQuantity,
            Reason = "Restock",
            Notes = request.Notes
        }, ct);

        item = await _repository.GetItemAsync(itemId, ct) ?? item;
        await EvaluateLowStockAsync(item, ct);
        await EvaluateExpiringSoonAsync(item, ct);
        return ToBatchDto(batch);
    }

    public async Task<InventoryBatchDto?> UpdateBatchMetadataAsync(
        Guid batchId,
        InventoryBatchMetadataRequest request,
        CancellationToken ct = default)
    {
        var items = await _repository.GetItemsAsync(includeArchived: true, ct);
        var batch = items.SelectMany(i => i.Batches).FirstOrDefault(b => b.Id == batchId);
        if (batch is null)
            return null;

        batch.ReceivedAt = NormalizeDate(request.ReceivedAt) ?? batch.ReceivedAt;
        batch.ExpiresAt = NormalizeDate(request.ExpiresAt);
        batch.LotNumber = request.LotNumber;
        batch.StorageState = request.StorageState;
        batch.Notes = request.Notes;

        var updated = await _repository.UpdateBatchAsync(batch, ct);
        if (updated is null)
            return null;

        var item = await _repository.GetItemAsync(updated.InventoryItemId, ct);
        if (item is not null)
        {
            await EvaluateLowStockAsync(item, ct);
            await EvaluateExpiringSoonAsync(item, ct);
        }

        return ToBatchDto(updated);
    }

    public async Task<InventoryItemDetailDto?> ConsumeAsync(
        Guid itemId,
        InventoryConsumeRequest request,
        CancellationToken ct = default)
    {
        if (request.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(request));

        var item = await _repository.GetItemAsync(itemId, ct);
        if (item is null)
            return null;

        await ConsumeItemAsync(
            item,
            request.Quantity,
            InventoryTransactionType.ManualConsume,
            request.Reason ?? "Manual consume",
            sourceType: null,
            sourceId: null,
            sourceTimestamp: null,
            request.BatchId,
            request.Notes,
            ct);

        return await GetItemAsync(itemId, ct);
    }

    public async Task<InventoryItemDetailDto?> AdjustBatchAsync(
        Guid batchId,
        InventoryAdjustBatchRequest request,
        CancellationToken ct = default)
    {
        if (request.RemainingQuantity < 0)
            throw new ArgumentException("Remaining quantity cannot be negative.", nameof(request));

        var items = await _repository.GetItemsAsync(includeArchived: true, ct);
        var item = items.FirstOrDefault(i => i.Batches.Any(b => b.Id == batchId));
        var batch = item?.Batches.FirstOrDefault(b => b.Id == batchId);
        if (item is null || batch is null)
            return null;

        var delta = request.RemainingQuantity - batch.RemainingQuantity;
        batch.RemainingQuantity = request.RemainingQuantity;
        if (batch.ReceivedQuantity < request.RemainingQuantity)
            batch.ReceivedQuantity = request.RemainingQuantity;

        await _repository.UpdateBatchAsync(batch, ct);
        await _repository.AddTransactionAsync(new InventoryTransactionEntity
        {
            InventoryItemId = item.Id,
            InventoryBatchId = batch.Id,
            Type = InventoryTransactionType.Adjustment,
            QuantityDelta = delta,
            QuantityAfter = batch.RemainingQuantity,
            Reason = request.Reason ?? "Manual adjustment",
            Notes = request.Notes
        }, ct);

        item = await _repository.GetItemAsync(item.Id, ct) ?? item;
        await EvaluateLowStockAsync(item, ct);
        await EvaluateExpiringSoonAsync(item, ct);
        return await GetItemAsync(item.Id, ct);
    }

    public async Task<InventoryBatchDto?> TransferBatchToExpiredAsync(
        Guid batchId,
        string? notes,
        CancellationToken ct = default)
    {
        var items = await _repository.GetItemsAsync(includeArchived: true, ct);
        var item = items.FirstOrDefault(i => i.Batches.Any(b => b.Id == batchId));
        var batch = item?.Batches.FirstOrDefault(b => b.Id == batchId);
        if (item is null || batch is null)
            return null;

        var delta = -batch.RemainingQuantity;
        batch.RemainingQuantity = 0;
        batch.StorageState = InventoryStorageState.Discarded;
        await _repository.UpdateBatchAsync(batch, ct);
        await _repository.AddTransactionAsync(new InventoryTransactionEntity
        {
            InventoryItemId = item.Id,
            InventoryBatchId = batch.Id,
            Type = InventoryTransactionType.Expired,
            QuantityDelta = delta,
            QuantityAfter = 0,
            Reason = "Marked expired/discarded",
            Notes = notes
        }, ct);

        item = await _repository.GetItemAsync(item.Id, ct) ?? item;
        await EvaluateLowStockAsync(item, ct);
        await EvaluateExpiringSoonAsync(item, ct);
        return ToBatchDto(batch);
    }

    public async Task AutoConsumeForDeviceEventAsync(DeviceEvent deviceEvent, CancellationToken ct = default)
    {
        if (deviceEvent.Id == Guid.Empty || string.IsNullOrWhiteSpace(deviceEvent.EventType.ToString()))
            return;

        var items = await _repository.FindDeviceEventItemsAsync(deviceEvent.EventType.ToString(), ct);
        foreach (var item in items)
        {
            await ConsumeItemAsync(
                item,
                1,
                InventoryTransactionType.AutoConsume,
                $"Device event {deviceEvent.EventType}",
                "device-event",
                deviceEvent.Id.ToString(),
                deviceEvent.Timestamp,
                batchId: null,
                notes: null,
                ct);

            // Linked-insulin drain: pump/pod items configured with a link also
            // consume from the bottle they're filled from. Same source_id so
            // ReverseSourceAsync reverses both in a single call.
            if (item.LinkedInsulinItemId.HasValue
                && item.LinkedInsulinUnitsPerUse is { } units && units > 0)
            {
                var linkedInsulin = await _repository.GetItemAsync(item.LinkedInsulinItemId.Value, ct);
                if (linkedInsulin is not null)
                {
                    await ConsumeItemAsync(
                        linkedInsulin,
                        units,
                        InventoryTransactionType.AutoConsume,
                        $"Device event {deviceEvent.EventType} (linked from {item.Name})",
                        "device-event",
                        deviceEvent.Id.ToString(),
                        deviceEvent.Timestamp,
                        batchId: null,
                        notes: null,
                        ct);
                }
                else
                {
                    _logger.LogDebug(
                        "Linked insulin item {LinkedId} not found for inventory item {ItemId}; skipping insulin drain",
                        item.LinkedInsulinItemId.Value, item.Id);
                }
            }
        }
    }

    public async Task AutoConsumeForBolusAsync(
        Bolus bolus,
        Guid? requestedPatientInsulinId,
        CancellationToken ct = default)
    {
        var quantity = (decimal)(bolus.Delivered ?? bolus.Insulin);
        if (bolus.Id == Guid.Empty || quantity <= 0)
            return;

        var insulinId = requestedPatientInsulinId ?? bolus.InsulinContext?.PatientInsulinId;
        if (!insulinId.HasValue)
            insulinId = (await _patientInsulins.GetPrimaryBolusInsulinAsync(ct))?.Id;

        var item = await _repository.FindAutoConsumeItemAsync(
            InventoryKind.Insulin,
            InventoryAutoConsumeSource.Bolus,
            insulinId,
            ct);

        if (item is null)
        {
            _logger.LogDebug("No inventory insulin item matched bolus {BolusId}", bolus.Id);
            return;
        }

        await ConsumeItemAsync(item, quantity, InventoryTransactionType.AutoConsume, "Bolus insulin", "bolus", bolus.Id.ToString(), bolus.Timestamp, null, null, ct);
    }

    public async Task AutoConsumeForBasalInjectionAsync(
        BasalInjection basalInjection,
        CancellationToken ct = default)
    {
        if (basalInjection.Id == Guid.Empty || basalInjection.Units <= 0)
            return;

        var insulinId = basalInjection.InsulinContext?.PatientInsulinId
                        ?? (await _patientInsulins.GetPrimaryBasalInsulinAsync(ct))?.Id;

        var item = await _repository.FindAutoConsumeItemAsync(
            InventoryKind.Insulin,
            InventoryAutoConsumeSource.BasalInjection,
            insulinId,
            ct);

        if (item is null)
        {
            _logger.LogDebug("No inventory insulin item matched basal injection {BasalInjectionId}", basalInjection.Id);
            return;
        }

        await ConsumeItemAsync(
            item,
            (decimal)basalInjection.Units,
            InventoryTransactionType.AutoConsume,
            "Basal injection insulin",
            "basal-injection",
            basalInjection.Id.ToString(),
            basalInjection.Timestamp,
            null,
            null,
            ct);
    }

    public async Task ReverseSourceAsync(string sourceType, string sourceId, CancellationToken ct = default)
    {
        var sourceTransactions = await _repository.GetSourceTransactionsAsync(sourceType, sourceId, ct);
        foreach (var transaction in sourceTransactions.Where(t => t.Type != InventoryTransactionType.Reversal))
        {
            var reversalSourceId = $"{sourceId}:reversal:{transaction.Id}";
            if ((await _repository.GetSourceTransactionsAsync(sourceType, reversalSourceId, ct)).Count > 0)
                continue;

            var allocations = DeserializeAllocations(transaction.Notes);
            if (allocations.Count == 0)
                continue;

            var item = transaction.Item;
            var batches = await _repository.GetBatchesAsync(item.Id, ct);
            foreach (var allocation in allocations)
            {
                var batch = batches.FirstOrDefault(b => b.Id == allocation.BatchId);
                if (batch is null)
                    continue;

                batch.RemainingQuantity += allocation.Quantity;
                if (batch.ReceivedQuantity < batch.RemainingQuantity)
                    batch.ReceivedQuantity = batch.RemainingQuantity;
                await _repository.UpdateBatchAsync(batch, ct);
            }

            var refreshed = await _repository.GetItemAsync(item.Id, ct) ?? item;
            await _repository.AddTransactionAsync(new InventoryTransactionEntity
            {
                InventoryItemId = item.Id,
                Type = InventoryTransactionType.Reversal,
                QuantityDelta = Math.Abs(transaction.QuantityDelta),
                QuantityAfter = CalculateUsableStock(refreshed.Batches),
                Reason = $"Reversed {sourceType}",
                SourceType = sourceType,
                SourceId = reversalSourceId,
                SourceTimestamp = DateTime.UtcNow,
                Notes = transaction.Notes
            }, ct);
            await EvaluateLowStockAsync(refreshed, ct);
            await EvaluateExpiringSoonAsync(refreshed, ct);
        }
    }

    private async Task ConsumeItemAsync(
        InventoryItemEntity item,
        decimal requestedQuantity,
        InventoryTransactionType transactionType,
        string reason,
        string? sourceType,
        string? sourceId,
        DateTime? sourceTimestamp,
        Guid? batchId,
        string? notes,
        CancellationToken ct)
    {
        if (sourceType is not null && sourceId is not null
            && await _repository.HasSourceTransactionAsync(item.Id, sourceType, sourceId, ct))
        {
            return;
        }

        var batches = await _repository.GetBatchesAsync(item.Id, ct);
        var candidates = batchId.HasValue
            ? batches.Where(b => b.Id == batchId.Value).ToList()
            : batches
                .Where(IsUsableBatch)
                .OrderBy(b => b.ExpiresAt == null)
                .ThenBy(b => b.ExpiresAt)
                .ThenBy(b => b.ReceivedAt)
                .ThenBy(b => b.Id)
                .ToList();

        var remaining = requestedQuantity;
        var allocations = new List<BatchAllocation>();
        foreach (var batch in candidates)
        {
            if (remaining <= 0)
                break;
            if (batch.RemainingQuantity <= 0)
                continue;

            var consumed = Math.Min(batch.RemainingQuantity, remaining);
            batch.RemainingQuantity -= consumed;
            remaining -= consumed;
            allocations.Add(new BatchAllocation(batch.Id, consumed));
            await _repository.UpdateBatchAsync(batch, ct);
        }

        var actualConsumed = requestedQuantity - remaining;
        var refreshed = await _repository.GetItemAsync(item.Id, ct) ?? item;
        await _repository.AddTransactionAsync(new InventoryTransactionEntity
        {
            InventoryItemId = item.Id,
            InventoryBatchId = batchId,
            Type = transactionType,
            QuantityDelta = -actualConsumed,
            QuantityAfter = CalculateUsableStock(refreshed.Batches),
            Reason = remaining > 0 ? $"{reason} (partial)" : reason,
            SourceType = sourceType,
            SourceId = sourceId,
            SourceTimestamp = sourceTimestamp,
            Notes = SerializeConsumptionNotes(notes, requestedQuantity, actualConsumed, remaining, allocations)
        }, ct);

        await EvaluateLowStockAsync(refreshed, ct);
        await EvaluateExpiringSoonAsync(refreshed, ct);
    }

    private async Task EvaluateLowStockAsync(InventoryItemEntity item, CancellationToken ct)
    {
        var ownerId = await GetTenantOwnerSubjectIdAsync(ct);
        if (ownerId is null) return;

        var usable = CalculateUsableStock(item.Batches);
        if (item.IsArchived || usable > item.LowStockThreshold)
        {
            await _notifications.ArchiveBySourceAsync(ownerId, LowStockNotificationType, item.Id.ToString(), NotificationArchiveReason.Completed, ct);
            return;
        }

        try
        {
            var existing = await _notificationRepository.FindBySourceAsync(
                ownerId,
                LowStockNotificationType,
                item.Id.ToString(),
                ct);
            if (existing is not null)
                return;

            await _notifications.CreateNotificationAsync(
                ownerId,
                LowStockNotificationType,
                $"{item.Name} low",
                subtitle: $"{usable:0.##} {item.UnitLabel} remaining",
                sourceId: item.Id.ToString(),
                actions:
                [
                    new NotificationActionDto
                    {
                        ActionId = "navigate",
                        Label = "Open inventory",
                        Icon = "package-search",
                        Variant = "default"
                    },
                    new NotificationActionDto
                    {
                        ActionId = "dismiss",
                        Label = "Dismiss",
                        Icon = "x",
                        Variant = "outline"
                    }
                ],
                metadata: new Dictionary<string, object>
                {
                    ["href"] = $"/settings/inventory?item={item.Id}",
                    ["itemId"] = item.Id,
                    ["usableStock"] = usable,
                    ["lowStockThreshold"] = item.LowStockThreshold
                },
                cancellationToken: ct);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogDebug(ex, "Inventory low-stock notification already rate-limited for {ItemId}", item.Id);
        }
    }

    private async Task EvaluateExpiringSoonAsync(InventoryItemEntity item, CancellationToken ct)
    {
        if (item.IsArchived) return;
        var ownerId = await GetTenantOwnerSubjectIdAsync(ct);
        if (ownerId is null) return;

        var now = DateTime.UtcNow;
        var threshold = now.AddDays(_options.ExpirySoonThresholdDays);

        foreach (var batch in item.Batches)
        {
            var batchSourceId = batch.Id.ToString();
            var withinWindow = IsUsableBatch(batch)
                               && batch.ExpiresAt.HasValue
                               && batch.ExpiresAt.Value <= threshold;

            if (!withinWindow)
            {
                await _notifications.ArchiveBySourceAsync(ownerId, ExpiringSoonNotificationType, batchSourceId, NotificationArchiveReason.Completed, ct);
                continue;
            }

            try
            {
                var existing = await _notificationRepository.FindBySourceAsync(
                    ownerId,
                    ExpiringSoonNotificationType,
                    batchSourceId,
                    ct);
                if (existing is not null)
                    continue;

                await _notifications.CreateNotificationAsync(
                    ownerId,
                    ExpiringSoonNotificationType,
                    $"{item.Name} expiring",
                    subtitle: $"{batch.RemainingQuantity:0.##} {item.UnitLabel} expires {batch.ExpiresAt:yyyy-MM-dd}",
                    sourceId: batchSourceId,
                    actions:
                    [
                        new NotificationActionDto
                        {
                            ActionId = "navigate",
                            Label = "Open inventory",
                            Icon = "package-search",
                            Variant = "default"
                        },
                        new NotificationActionDto
                        {
                            ActionId = "dismiss",
                            Label = "Dismiss",
                            Icon = "x",
                            Variant = "outline"
                        }
                    ],
                    metadata: new Dictionary<string, object>
                    {
                        ["href"] = $"/settings/inventory?item={item.Id}&batch={batch.Id}",
                        ["itemId"] = item.Id,
                        ["batchId"] = batch.Id,
                        ["expiresAt"] = batch.ExpiresAt!.Value
                    },
                    cancellationToken: ct);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogDebug(ex, "Inventory expiring-soon notification already rate-limited for batch {BatchId}", batch.Id);
            }
        }
    }

    private Task<string?> GetTenantOwnerSubjectIdAsync(CancellationToken ct)
        => _tenantOwnerResolver.GetCurrentTenantOwnerSubjectIdAsync(ct);

    private async Task TryAutoLinkReservoirsToSoleInsulinAsync(CancellationToken ct)
    {
        var items = await _repository.GetItemsAsync(includeArchived: false, ct);
        var insulinItems = items.Where(i => i.Kind == InventoryKind.Insulin).ToList();
        if (insulinItems.Count != 1) return;
        var insulinId = insulinItems[0].Id;

        foreach (var item in items)
        {
            if (item.Kind is not (InventoryKind.Pod or InventoryKind.Reservoir)) continue;
            if (item.LinkedInsulinItemId.HasValue) continue;
            if (!item.LinkedInsulinUnitsPerUse.HasValue) continue;

            item.LinkedInsulinItemId = insulinId;
            await _repository.UpdateItemAsync(item, ct);
        }
    }

    private static void ValidateItemRequest(InventoryItemRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name is required.", nameof(request));
        if (request.LowStockThreshold < 0)
            throw new ArgumentException("Low stock threshold cannot be negative.", nameof(request));
        if (request.TargetStock is < 0)
            throw new ArgumentException("Target stock cannot be negative.", nameof(request));
        if (request.LinkedInsulinUnitsPerUse is < 0)
            throw new ArgumentException("Linked insulin units per use cannot be negative.", nameof(request));
    }

    private static string NormalizeUnit(string? unitLabel) => string.IsNullOrWhiteSpace(unitLabel) ? "each" : unitLabel.Trim();

    private static DateTime? NormalizeDate(DateTime? value)
        => value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : null;

    private static bool IsUsableBatch(InventoryBatchEntity batch)
        => batch.RemainingQuantity > 0
           && batch.StorageState != InventoryStorageState.Discarded
           && batch.StorageState != InventoryStorageState.Frozen
           && batch.StorageState != InventoryStorageState.HeatExposed
           && (!batch.ExpiresAt.HasValue || batch.ExpiresAt.Value.Date >= DateTime.UtcNow.Date);

    private static decimal CalculateUsableStock(IEnumerable<InventoryBatchEntity> batches)
        => batches.Where(IsUsableBatch).Sum(b => b.RemainingQuantity);

    private static InventoryItemDto ToItemDto(
        InventoryItemEntity item,
        int expirySoonThresholdDays,
        IReadOnlyDictionary<Guid, InventoryItemEntity>? itemsById = null,
        IReadOnlyDictionary<Guid, IReadOnlyList<InventoryTransactionEntity>>? consumesByItem = null)
    {
        var now = DateTime.UtcNow;
        var soonThreshold = now.AddDays(expirySoonThresholdDays);
        var total = item.Batches.Where(b => b.StorageState != InventoryStorageState.Discarded).Sum(b => b.RemainingQuantity);
        var usable = CalculateUsableStock(item.Batches);
        var expired = item.Batches
            .Where(b => b.RemainingQuantity > 0 && b.ExpiresAt.HasValue && b.ExpiresAt.Value.Date < now.Date)
            .Sum(b => b.RemainingQuantity);
        var expiringSoon = item.Batches
            .Where(b => IsUsableBatch(b) && b.ExpiresAt.HasValue && b.ExpiresAt.Value <= soonThreshold)
            .Sum(b => b.RemainingQuantity);
        var lowestExpiry = item.Batches
            .Where(IsUsableBatch)
            .Select(b => b.ExpiresAt)
            .Where(d => d.HasValue)
            .OrderBy(d => d)
            .FirstOrDefault();

        var consumes = consumesByItem != null && consumesByItem.TryGetValue(item.Id, out var c)
            ? c
            : (IReadOnlyList<InventoryTransactionEntity>)Array.Empty<InventoryTransactionEntity>();
        var (runOut, runOutSource) = ProjectRunOut(item, usable, itemsById, consumes, now);

        return new InventoryItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Category = item.Category,
            Kind = item.Kind,
            UnitLabel = item.UnitLabel,
            LowStockThreshold = item.LowStockThreshold,
            TargetStock = item.TargetStock,
            AutoConsumeEnabled = item.AutoConsumeEnabled,
            AutoConsumeSource = item.AutoConsumeSource,
            PatientInsulinId = item.PatientInsulinId,
            DeviceEventTypes = DeserializeEventTypes(item.DeviceEventTypesJson),
            LinkedInsulinItemId = item.LinkedInsulinItemId,
            LinkedInsulinUnitsPerUse = item.LinkedInsulinUnitsPerUse,
            WearDays = item.WearDays,
            IsDefault = item.IsDefault,
            IsArchived = item.IsArchived,
            TotalStock = total,
            UsableStock = usable,
            ExpiredStock = expired,
            ExpiringSoonStock = expiringSoon,
            LowestExpiry = lowestExpiry,
            IsLow = !item.IsArchived && usable <= item.LowStockThreshold,
            SuggestedRestockQuantity = item.TargetStock.HasValue ? Math.Max(0, item.TargetStock.Value - usable) : 0,
            EstimatedRunOutAt = runOut,
            RunOutProjectionSource = runOutSource
        };
    }

    /// <summary>
    /// Projects the date at which an item's usable stock will reach zero.
    /// Returns null when no projection is possible.
    ///
    /// Strategy order (first match wins):
    /// 1. LinkedItem — pump-mode insulin tied to a Pod/Reservoir item with wear-time
    /// 2. WearTime — item has a fixed wear duration (sensors, pods, infusion sets)
    /// 3. HistoricalRate — average daily consumption over the recent ledger window
    /// </summary>
    internal static (DateTime? runOut, RunOutProjectionSource? source) ProjectRunOut(
        InventoryItemEntity item,
        decimal usable,
        IReadOnlyDictionary<Guid, InventoryItemEntity>? itemsById,
        IReadOnlyList<InventoryTransactionEntity> recentConsumes,
        DateTime now)
    {
        if (item.IsArchived || usable <= 0)
            return (null, null);

        // Strategy 1: LinkedItem — for pump-mode insulin items linked from a
        // Pod/Reservoir, use the linked item's wear-time projection. The user
        // runs out of insulin either when the pods deplete OR when the bottle
        // depletes — whichever comes first.
        if (item.Kind == InventoryKind.Insulin && itemsById is not null)
        {
            var linkedFromPod = itemsById.Values.FirstOrDefault(i =>
                i.LinkedInsulinItemId == item.Id
                && i.LinkedInsulinUnitsPerUse.HasValue
                && i.LinkedInsulinUnitsPerUse.Value > 0
                && i.WearDays.HasValue
                && i.WearDays.Value > 0);

            if (linkedFromPod is not null)
            {
                var linkedUsable = CalculateUsableStock(linkedFromPod.Batches);
                var unitsPerUse = linkedFromPod.LinkedInsulinUnitsPerUse!.Value;
                var wearDays = linkedFromPod.WearDays!.Value;

                if (linkedUsable > 0)
                {
                    // Pods can support `linkedUsable * unitsPerUse` units of insulin draw.
                    var maxDrainFromLinked = linkedUsable * unitsPerUse;
                    var daysFromPods = linkedUsable * wearDays;
                    if (usable >= maxDrainFromLinked)
                    {
                        // Pods will run out before insulin
                        return (now.AddDays((double)daysFromPods), RunOutProjectionSource.LinkedItem);
                    }

                    // Insulin runs out before pods do
                    var insulinChangesRemaining = usable / unitsPerUse;
                    var daysUntilInsulinDepleted = insulinChangesRemaining * wearDays;
                    return (now.AddDays((double)daysUntilInsulinDepleted), RunOutProjectionSource.LinkedItem);
                }
            }
        }

        // Strategy 2: WearTime — deterministic for items with a known wear duration.
        if (item.WearDays.HasValue && item.WearDays.Value > 0)
        {
            var days = usable * item.WearDays.Value;
            return (now.AddDays((double)days), RunOutProjectionSource.WearTime);
        }

        // Strategy 3: HistoricalRate — average daily consumption over the ledger window.
        if (recentConsumes.Count >= MinConsumeTransactionsForProjection)
        {
            var totalConsumed = recentConsumes.Sum(t => Math.Abs(t.QuantityDelta));
            if (totalConsumed > 0)
            {
                var avgPerDay = totalConsumed / HistoricalConsumeWindowDays;
                if (avgPerDay > 0)
                {
                    var daysRemaining = usable / avgPerDay;
                    return (now.AddDays((double)daysRemaining), RunOutProjectionSource.HistoricalRate);
                }
            }
        }

        return (null, null);
    }

    private static InventoryItemDetailDto ToDetailDto(
        InventoryItemEntity item,
        IEnumerable<InventoryTransactionEntity> transactions,
        int expirySoonThresholdDays,
        IReadOnlyDictionary<Guid, InventoryItemEntity>? itemsById = null,
        IReadOnlyDictionary<Guid, IReadOnlyList<InventoryTransactionEntity>>? consumesByItem = null)
    {
        var baseDto = ToItemDto(item, expirySoonThresholdDays, itemsById, consumesByItem);
        return new InventoryItemDetailDto
        {
            Id = baseDto.Id,
            Name = baseDto.Name,
            Category = baseDto.Category,
            Kind = baseDto.Kind,
            UnitLabel = baseDto.UnitLabel,
            LowStockThreshold = baseDto.LowStockThreshold,
            TargetStock = baseDto.TargetStock,
            AutoConsumeEnabled = baseDto.AutoConsumeEnabled,
            AutoConsumeSource = baseDto.AutoConsumeSource,
            PatientInsulinId = baseDto.PatientInsulinId,
            DeviceEventTypes = baseDto.DeviceEventTypes,
            LinkedInsulinItemId = baseDto.LinkedInsulinItemId,
            LinkedInsulinUnitsPerUse = baseDto.LinkedInsulinUnitsPerUse,
            WearDays = baseDto.WearDays,
            IsDefault = baseDto.IsDefault,
            IsArchived = baseDto.IsArchived,
            TotalStock = baseDto.TotalStock,
            UsableStock = baseDto.UsableStock,
            ExpiredStock = baseDto.ExpiredStock,
            ExpiringSoonStock = baseDto.ExpiringSoonStock,
            LowestExpiry = baseDto.LowestExpiry,
            IsLow = baseDto.IsLow,
            SuggestedRestockQuantity = baseDto.SuggestedRestockQuantity,
            EstimatedRunOutAt = baseDto.EstimatedRunOutAt,
            RunOutProjectionSource = baseDto.RunOutProjectionSource,
            Batches = item.Batches
                .OrderBy(b => b.ExpiresAt == null)
                .ThenBy(b => b.ExpiresAt)
                .ThenBy(b => b.ReceivedAt)
                .ThenBy(b => b.Id)
                .Select(ToBatchDto)
                .ToList(),
            Transactions = transactions.Select(ToTransactionDto).ToList()
        };
    }

    private static InventoryBatchDto ToBatchDto(InventoryBatchEntity batch) => new()
    {
        Id = batch.Id,
        InventoryItemId = batch.InventoryItemId,
        ReceivedQuantity = batch.ReceivedQuantity,
        RemainingQuantity = batch.RemainingQuantity,
        ReceivedAt = batch.ReceivedAt,
        ExpiresAt = batch.ExpiresAt,
        LotNumber = batch.LotNumber,
        StorageState = batch.StorageState,
        Notes = batch.Notes,
        IsExpired = batch.ExpiresAt.HasValue && batch.ExpiresAt.Value.Date < DateTime.UtcNow.Date,
        IsUsable = IsUsableBatch(batch)
    };

    private static InventoryTransactionDto ToTransactionDto(InventoryTransactionEntity transaction) => new()
    {
        Id = transaction.Id,
        InventoryItemId = transaction.InventoryItemId,
        InventoryBatchId = transaction.InventoryBatchId,
        Type = transaction.Type,
        QuantityDelta = transaction.QuantityDelta,
        QuantityAfter = transaction.QuantityAfter,
        Reason = transaction.Reason,
        SourceType = transaction.SourceType,
        SourceId = transaction.SourceId,
        SourceTimestamp = transaction.SourceTimestamp,
        Notes = transaction.Notes,
        CreatedAt = transaction.CreatedAt
    };

    private static string[] DeserializeEventTypes(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return [];
        try
        {
            return JsonSerializer.Deserialize<string[]>(json, JsonOptions) ?? [];
        }
        catch
        {
            return [];
        }
    }

    private static string SerializeConsumptionNotes(
        string? notes,
        decimal requestedQuantity,
        decimal consumedQuantity,
        decimal shortfall,
        List<BatchAllocation> allocations)
    {
        return JsonSerializer.Serialize(new ConsumptionLedgerNotes(
            notes,
            requestedQuantity,
            consumedQuantity,
            shortfall,
            allocations), JsonOptions);
    }

    private static List<BatchAllocation> DeserializeAllocations(string? notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
            return [];
        try
        {
            return JsonSerializer.Deserialize<ConsumptionLedgerNotes>(notes, JsonOptions)?.Allocations ?? [];
        }
        catch
        {
            return [];
        }
    }

    private record BatchAllocation(Guid BatchId, decimal Quantity);
    private record ConsumptionLedgerNotes(
        string? Notes,
        decimal RequestedQuantity,
        decimal ConsumedQuantity,
        decimal Shortfall,
        List<BatchAllocation> Allocations);

    // ── Generic supplies ───────────────────────────────────────────────
    // Seeded unconditionally on top of whatever the user picks from the
    // device catalog. Brand granularity isn't useful for stock tracking
    // on these — a lancet is a lancet.
    private record GenericItemSpec(
        InventoryKind Kind,
        string Name,
        InventoryCategory InventoryCategory,
        string UnitLabel,
        decimal LowStockThreshold,
        decimal? TargetStock);

    private static readonly GenericItemSpec[] GenericSupplyItems =
    [
        new(InventoryKind.TestStrip, "Test strips", InventoryCategory.Testing, "strips", 25, 100),
        new(InventoryKind.Lancet, "Lancets", InventoryCategory.Testing, "lancets", 25, 100),
        new(InventoryKind.AlcoholSwab, "Alcohol swabs", InventoryCategory.Testing, "swabs", 25, 100),
        new(InventoryKind.ControlSolution, "Control solution", InventoryCategory.Testing, "bottles", 1, 1),
        new(InventoryKind.Glucagon, "Glucagon", InventoryCategory.Emergency, "kits", 1, 1),
        new(InventoryKind.FastCarbs, "Glucose tabs / juice", InventoryCategory.Emergency, "servings", 5, 15),
        new(InventoryKind.KetoneStrip, "Ketone strips", InventoryCategory.Emergency, "strips", 10, 25)
    ];
}
