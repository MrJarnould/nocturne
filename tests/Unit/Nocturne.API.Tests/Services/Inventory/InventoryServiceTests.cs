using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Nocturne.API.Services.Inventory;
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
using Xunit;

namespace Nocturne.API.Tests.Services.Inventory;

/// <summary>
/// Critical-path unit tests for <see cref="InventoryService"/>. Covers
/// FEFO allocation, reversal, idempotency, linked-insulin drain,
/// damage-state exclusion, and default-catalog topology per therapy mode.
///
/// Notification firing is mocked via <see cref="IInAppNotificationService"/>;
/// integration tests using Testcontainers cover the cross-table state.
/// </summary>
public class InventoryServiceTests
{
    private readonly Mock<IInventoryRepository> _repo = new();
    private readonly Mock<IInAppNotificationRepository> _notificationRepo = new();
    private readonly Mock<IInAppNotificationService> _notifications = new();
    private readonly Mock<IPatientInsulinRepository> _patientInsulins = new();
    private readonly Mock<ITenantOwnerResolver> _ownerResolver = new();
    private readonly InventoryOptions _options = new() { ExpirySoonThresholdDays = 30 };
    private readonly Mock<ILogger<InventoryService>> _logger = new();

    // Persisted-tx capture so we can assert against the ledger row produced.
    private readonly List<InventoryTransactionEntity> _ledger = new();
    // Persisted-batch updates so reversal can replay against current batch state.
    private readonly Dictionary<Guid, InventoryBatchEntity> _batchStore = new();

    // Captured items indexed by id so AddTransactionAsync can wire navigation back.
    private readonly Dictionary<Guid, InventoryItemEntity> _itemStore = new();

    private InventoryService NewService(string? ownerSubject = "00000000-0000-0000-0000-000000000001")
    {
        _ownerResolver
            .Setup(o => o.GetCurrentTenantOwnerSubjectIdAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(ownerSubject);

        _repo.Setup(r => r.AddTransactionAsync(It.IsAny<InventoryTransactionEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryTransactionEntity tx, CancellationToken _) =>
            {
                tx.Id = tx.Id == Guid.Empty ? Guid.NewGuid() : tx.Id;
                tx.CreatedAt = tx.CreatedAt == default ? DateTime.UtcNow.AddTicks(_ledger.Count) : tx.CreatedAt;
                if (tx.Item is null && _itemStore.TryGetValue(tx.InventoryItemId, out var navItem))
                {
                    tx.Item = navItem;
                }
                _ledger.Add(tx);
                return tx;
            });

        // Default: no transactions for any item.
        _repo.Setup(r => r.GetTransactionsAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid itemId, int _, CancellationToken __) =>
                _ledger.Where(t => t.InventoryItemId == itemId).ToList());

        _repo.Setup(r => r.UpdateBatchAsync(It.IsAny<InventoryBatchEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryBatchEntity b, CancellationToken _) =>
            {
                _batchStore[b.Id] = b;
                return b;
            });

        _repo.Setup(r => r.GetSourceTransactionsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string srcType, string srcId, CancellationToken _) =>
                _ledger.Where(t => t.SourceType == srcType && t.SourceId == srcId).OrderBy(t => t.CreatedAt).ToList());

        _repo.Setup(r => r.HasSourceTransactionAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid itemId, string srcType, string srcId, CancellationToken _) =>
                _ledger.Any(t => t.InventoryItemId == itemId && t.SourceType == srcType && t.SourceId == srcId
                                 && t.Type != InventoryTransactionType.Reversal));

        return new InventoryService(
            _repo.Object,
            _notificationRepo.Object,
            _notifications.Object,
            _patientInsulins.Object,
            _ownerResolver.Object,
            Options.Create(_options),
            _logger.Object);
    }

    private static InventoryBatchEntity Batch(Guid itemId, decimal remaining, DateTime? expires, DateTime received, InventoryStorageState state = InventoryStorageState.Normal)
        => new()
        {
            Id = Guid.NewGuid(),
            InventoryItemId = itemId,
            ReceivedQuantity = remaining,
            RemainingQuantity = remaining,
            ReceivedAt = received,
            ExpiresAt = expires,
            StorageState = state,
        };

    private InventoryItemEntity Item(InventoryKind kind, InventoryAutoConsumeSource source = InventoryAutoConsumeSource.None, List<InventoryBatchEntity>? batches = null)
    {
        var entity = new InventoryItemEntity
        {
            Id = Guid.NewGuid(),
            Name = $"{kind} item",
            Kind = kind,
            UnitLabel = "units",
            AutoConsumeEnabled = true,
            AutoConsumeSource = source,
            LowStockThreshold = 0,
            Batches = batches ?? new List<InventoryBatchEntity>(),
        };
        _itemStore[entity.Id] = entity;
        return entity;
    }

    private void RegisterBatches(IEnumerable<InventoryBatchEntity> batches)
    {
        foreach (var b in batches) _batchStore[b.Id] = b;
    }

    // ── FEFO allocation ────────────────────────────────────────────────

    [Fact]
    public async Task Consume_AllocatesEarliestExpiryFirst()
    {
        var item = Item(InventoryKind.TestStrip);
        var laterExpiry = Batch(item.Id, remaining: 50, expires: new DateTime(2027, 1, 1), received: new DateTime(2026, 1, 1));
        var earlierExpiry = Batch(item.Id, remaining: 50, expires: new DateTime(2026, 6, 1), received: new DateTime(2026, 2, 1));
        item.Batches = new List<InventoryBatchEntity> { laterExpiry, earlierExpiry };
        RegisterBatches(item.Batches);
        _repo.Setup(r => r.GetItemAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _repo.Setup(r => r.GetBatchesAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item.Batches.ToList());

        var svc = NewService();
        await svc.ConsumeAsync(item.Id, new InventoryConsumeRequest(20, "manual", null, null));

        earlierExpiry.RemainingQuantity.Should().Be(30, "earlier-expiry batch consumed first");
        laterExpiry.RemainingQuantity.Should().Be(50);
    }

    [Fact]
    public async Task Consume_DatedBatchesBeforeUndatedBatches()
    {
        var item = Item(InventoryKind.TestStrip);
        var undated = Batch(item.Id, remaining: 50, expires: null, received: new DateTime(2026, 1, 1));
        var dated = Batch(item.Id, remaining: 50, expires: new DateTime(2027, 1, 1), received: new DateTime(2026, 6, 1));
        item.Batches = new List<InventoryBatchEntity> { undated, dated };
        RegisterBatches(item.Batches);
        _repo.Setup(r => r.GetItemAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _repo.Setup(r => r.GetBatchesAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item.Batches.ToList());

        var svc = NewService();
        await svc.ConsumeAsync(item.Id, new InventoryConsumeRequest(10, null, null, null));

        dated.RemainingQuantity.Should().Be(40, "dated batches consumed before undated ones");
        undated.RemainingQuantity.Should().Be(50);
    }

    [Fact]
    public async Task Consume_SpansMultipleBatches_WhenSingleNotEnough()
    {
        var item = Item(InventoryKind.TestStrip);
        var first = Batch(item.Id, remaining: 30, expires: new DateTime(2026, 6, 1), received: new DateTime(2026, 1, 1));
        var second = Batch(item.Id, remaining: 50, expires: new DateTime(2026, 9, 1), received: new DateTime(2026, 2, 1));
        item.Batches = new List<InventoryBatchEntity> { first, second };
        RegisterBatches(item.Batches);
        _repo.Setup(r => r.GetItemAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _repo.Setup(r => r.GetBatchesAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item.Batches.ToList());

        var svc = NewService();
        await svc.ConsumeAsync(item.Id, new InventoryConsumeRequest(60, null, null, null));

        first.RemainingQuantity.Should().Be(0, "first batch drained");
        second.RemainingQuantity.Should().Be(20, "remaining 30 came from second batch");
    }

    // ── Reversal ───────────────────────────────────────────────────────

    [Fact]
    public async Task Reverse_RestoresBatchesPerLedger()
    {
        var item = Item(InventoryKind.Insulin);
        var batch = Batch(item.Id, remaining: 100, expires: null, received: new DateTime(2026, 1, 1));
        item.Batches = new List<InventoryBatchEntity> { batch };
        RegisterBatches(item.Batches);
        _repo.Setup(r => r.GetItemAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _repo.Setup(r => r.GetBatchesAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item.Batches.ToList());

        _repo.Setup(r => r.FindAutoConsumeItemAsync(InventoryKind.Insulin, InventoryAutoConsumeSource.Bolus, It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);

        var svc = NewService();
        var bolusId = Guid.NewGuid();
        await svc.AutoConsumeForBolusAsync(
            new Bolus { Id = bolusId, Insulin = 5, Delivered = 5, Timestamp = DateTime.UtcNow },
            requestedPatientInsulinId: null);
        batch.RemainingQuantity.Should().Be(95, "5 units consumed");

        await svc.ReverseSourceAsync("bolus", bolusId.ToString());

        batch.RemainingQuantity.Should().Be(100, "reversal restored to original");
        _ledger.Should().Contain(t => t.Type == InventoryTransactionType.Reversal);
    }

    [Fact]
    public async Task Reverse_IsIdempotent()
    {
        var item = Item(InventoryKind.Insulin);
        var batch = Batch(item.Id, remaining: 100, expires: null, received: new DateTime(2026, 1, 1));
        item.Batches = new List<InventoryBatchEntity> { batch };
        RegisterBatches(item.Batches);
        _repo.Setup(r => r.GetItemAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _repo.Setup(r => r.GetBatchesAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item.Batches.ToList());
        _repo.Setup(r => r.FindAutoConsumeItemAsync(InventoryKind.Insulin, InventoryAutoConsumeSource.Bolus, It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);

        var svc = NewService();
        var bolusId = Guid.NewGuid();
        await svc.AutoConsumeForBolusAsync(
            new Bolus { Id = bolusId, Insulin = 3, Delivered = 3, Timestamp = DateTime.UtcNow },
            null);
        await svc.ReverseSourceAsync("bolus", bolusId.ToString());
        await svc.ReverseSourceAsync("bolus", bolusId.ToString());

        batch.RemainingQuantity.Should().Be(100, "second reverse is a no-op");
        _ledger.Count(t => t.Type == InventoryTransactionType.Reversal).Should().Be(1);
    }

    // ── Idempotency ────────────────────────────────────────────────────

    [Fact]
    public async Task AutoConsume_SameSourceTwice_OnlyConsumesOnce()
    {
        var item = Item(InventoryKind.Insulin);
        var batch = Batch(item.Id, remaining: 100, expires: null, received: new DateTime(2026, 1, 1));
        item.Batches = new List<InventoryBatchEntity> { batch };
        RegisterBatches(item.Batches);
        _repo.Setup(r => r.GetItemAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _repo.Setup(r => r.GetBatchesAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item.Batches.ToList());
        _repo.Setup(r => r.FindAutoConsumeItemAsync(InventoryKind.Insulin, InventoryAutoConsumeSource.Bolus, It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);

        var svc = NewService();
        var bolus = new Bolus { Id = Guid.NewGuid(), Insulin = 4, Delivered = 4, Timestamp = DateTime.UtcNow };
        await svc.AutoConsumeForBolusAsync(bolus, null);
        await svc.AutoConsumeForBolusAsync(bolus, null);

        batch.RemainingQuantity.Should().Be(96, "second call no-ops due to source idempotency");
    }

    // ── Linked-insulin drain ───────────────────────────────────────────

    [Fact]
    public async Task DeviceEvent_PodWithLinkedInsulin_DrainsBothItems()
    {
        var insulin = Item(InventoryKind.Insulin, InventoryAutoConsumeSource.None);
        var insulinBatch = Batch(insulin.Id, remaining: 1000, expires: null, received: new DateTime(2026, 1, 1));
        insulin.Batches = new List<InventoryBatchEntity> { insulinBatch };

        var pod = Item(InventoryKind.Pod, InventoryAutoConsumeSource.DeviceEvent);
        pod.DeviceEventTypesJson = "[\"PodChange\"]";
        pod.LinkedInsulinItemId = insulin.Id;
        pod.LinkedInsulinUnitsPerUse = 200;
        var podBatch = Batch(pod.Id, remaining: 5, expires: null, received: new DateTime(2026, 1, 1));
        pod.Batches = new List<InventoryBatchEntity> { podBatch };
        RegisterBatches(new[] { insulinBatch, podBatch });

        _repo.Setup(r => r.FindDeviceEventItemsAsync("PodChange", It.IsAny<CancellationToken>())).ReturnsAsync(new List<InventoryItemEntity> { pod });
        _repo.Setup(r => r.GetItemAsync(pod.Id, It.IsAny<CancellationToken>())).ReturnsAsync(pod);
        _repo.Setup(r => r.GetItemAsync(insulin.Id, It.IsAny<CancellationToken>())).ReturnsAsync(insulin);
        _repo.Setup(r => r.GetBatchesAsync(pod.Id, It.IsAny<CancellationToken>())).ReturnsAsync(pod.Batches.ToList());
        _repo.Setup(r => r.GetBatchesAsync(insulin.Id, It.IsAny<CancellationToken>())).ReturnsAsync(insulin.Batches.ToList());

        var svc = NewService();
        await svc.AutoConsumeForDeviceEventAsync(new DeviceEvent { Id = Guid.NewGuid(), EventType = DeviceEventType.PodChange, Timestamp = DateTime.UtcNow });

        podBatch.RemainingQuantity.Should().Be(4);
        insulinBatch.RemainingQuantity.Should().Be(800, "200u drained from linked insulin");
    }

    [Fact]
    public async Task DeviceEvent_PodWithoutLink_DoesNotTouchInsulin()
    {
        var insulin = Item(InventoryKind.Insulin, InventoryAutoConsumeSource.None);
        var insulinBatch = Batch(insulin.Id, remaining: 1000, expires: null, received: new DateTime(2026, 1, 1));
        insulin.Batches = new List<InventoryBatchEntity> { insulinBatch };

        var pod = Item(InventoryKind.Pod, InventoryAutoConsumeSource.DeviceEvent);
        pod.DeviceEventTypesJson = "[\"PodChange\"]";
        // No LinkedInsulinItemId set.
        var podBatch = Batch(pod.Id, remaining: 5, expires: null, received: new DateTime(2026, 1, 1));
        pod.Batches = new List<InventoryBatchEntity> { podBatch };
        RegisterBatches(new[] { insulinBatch, podBatch });

        _repo.Setup(r => r.FindDeviceEventItemsAsync("PodChange", It.IsAny<CancellationToken>())).ReturnsAsync(new List<InventoryItemEntity> { pod });
        _repo.Setup(r => r.GetItemAsync(pod.Id, It.IsAny<CancellationToken>())).ReturnsAsync(pod);
        _repo.Setup(r => r.GetBatchesAsync(pod.Id, It.IsAny<CancellationToken>())).ReturnsAsync(pod.Batches.ToList());

        var svc = NewService();
        await svc.AutoConsumeForDeviceEventAsync(new DeviceEvent { Id = Guid.NewGuid(), EventType = DeviceEventType.PodChange, Timestamp = DateTime.UtcNow });

        podBatch.RemainingQuantity.Should().Be(4);
        insulinBatch.RemainingQuantity.Should().Be(1000, "insulin untouched when no link configured");
    }

    [Fact]
    public async Task Reverse_DeviceEvent_RestoresBothPodAndLinkedInsulin()
    {
        var insulin = Item(InventoryKind.Insulin, InventoryAutoConsumeSource.None);
        var insulinBatch = Batch(insulin.Id, remaining: 1000, expires: null, received: new DateTime(2026, 1, 1));
        insulin.Batches = new List<InventoryBatchEntity> { insulinBatch };

        var pod = Item(InventoryKind.Pod, InventoryAutoConsumeSource.DeviceEvent);
        pod.DeviceEventTypesJson = "[\"PodChange\"]";
        pod.LinkedInsulinItemId = insulin.Id;
        pod.LinkedInsulinUnitsPerUse = 200;
        var podBatch = Batch(pod.Id, remaining: 5, expires: null, received: new DateTime(2026, 1, 1));
        pod.Batches = new List<InventoryBatchEntity> { podBatch };
        RegisterBatches(new[] { insulinBatch, podBatch });

        _repo.Setup(r => r.FindDeviceEventItemsAsync("PodChange", It.IsAny<CancellationToken>())).ReturnsAsync(new List<InventoryItemEntity> { pod });
        _repo.Setup(r => r.GetItemAsync(pod.Id, It.IsAny<CancellationToken>())).ReturnsAsync(pod);
        _repo.Setup(r => r.GetItemAsync(insulin.Id, It.IsAny<CancellationToken>())).ReturnsAsync(insulin);
        _repo.Setup(r => r.GetBatchesAsync(pod.Id, It.IsAny<CancellationToken>())).ReturnsAsync(pod.Batches.ToList());
        _repo.Setup(r => r.GetBatchesAsync(insulin.Id, It.IsAny<CancellationToken>())).ReturnsAsync(insulin.Batches.ToList());

        var svc = NewService();
        var eventId = Guid.NewGuid();
        await svc.AutoConsumeForDeviceEventAsync(new DeviceEvent { Id = eventId, EventType = DeviceEventType.PodChange, Timestamp = DateTime.UtcNow });
        await svc.ReverseSourceAsync("device-event", eventId.ToString());

        podBatch.RemainingQuantity.Should().Be(5);
        insulinBatch.RemainingQuantity.Should().Be(1000);
    }

    // ── Damage-state exclusion (IsUsableBatch via consumption behavior) ─

    [Fact]
    public async Task Consume_SkipsHeatExposedBatches()
        => await AssertBatchExcluded(InventoryStorageState.HeatExposed);

    [Fact]
    public async Task Consume_SkipsFrozenBatches()
        => await AssertBatchExcluded(InventoryStorageState.Frozen);

    [Fact]
    public async Task Consume_SkipsDiscardedBatches()
        => await AssertBatchExcluded(InventoryStorageState.Discarded);

    private async Task AssertBatchExcluded(InventoryStorageState state)
    {
        var item = Item(InventoryKind.Insulin);
        var damaged = Batch(item.Id, remaining: 100, expires: null, received: new DateTime(2026, 1, 1), state: state);
        var usable = Batch(item.Id, remaining: 100, expires: null, received: new DateTime(2026, 2, 1));
        item.Batches = new List<InventoryBatchEntity> { damaged, usable };
        RegisterBatches(item.Batches);
        _repo.Setup(r => r.GetItemAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item);
        _repo.Setup(r => r.GetBatchesAsync(item.Id, It.IsAny<CancellationToken>())).ReturnsAsync(item.Batches.ToList());

        var svc = NewService();
        await svc.ConsumeAsync(item.Id, new InventoryConsumeRequest(10, null, null, null));

        damaged.RemainingQuantity.Should().Be(100, $"{state} batch must not be consumed");
        usable.RemainingQuantity.Should().Be(90);
    }

    // ── Catalog topology by therapy mode ───────────────────────────────

    [Fact]
    public void Catalog_Pump_ExcludesBasalInsulins()
    {
        var svc = NewService();
        var catalog = svc.GetInventoryCatalog(TherapyMode.Pump);
        catalog.Should().NotContain(e => e.Category == InventoryCatalogCategory.BasalInsulin,
            "pump users don't take long-acting basal injections alongside the pump");
    }

    [Fact]
    public void Catalog_Pump_IncludesCgmsPumpsAndRapidInsulins()
    {
        var svc = NewService();
        var catalog = svc.GetInventoryCatalog(TherapyMode.Pump);
        catalog.Should().Contain(e => e.Category == InventoryCatalogCategory.Cgm);
        catalog.Should().Contain(e => e.Category == InventoryCatalogCategory.Pump);
        catalog.Should().Contain(e => e.Category == InventoryCatalogCategory.RapidInsulin);
    }

    [Fact]
    public void Catalog_Mdi_ExcludesPumps()
    {
        var svc = NewService();
        var catalog = svc.GetInventoryCatalog(TherapyMode.Mdi);
        catalog.Should().NotContain(e => e.Category == InventoryCatalogCategory.Pump,
            "MDI users don't have insulin pumps");
    }

    [Fact]
    public void Catalog_Mdi_IncludesCgmsRapidAndBasalInsulins()
    {
        var svc = NewService();
        var catalog = svc.GetInventoryCatalog(TherapyMode.Mdi);
        catalog.Should().Contain(e => e.Category == InventoryCatalogCategory.Cgm);
        catalog.Should().Contain(e => e.Category == InventoryCatalogCategory.RapidInsulin);
        catalog.Should().Contain(e => e.Category == InventoryCatalogCategory.BasalInsulin);
    }

    // ── Seed flow ──────────────────────────────────────────────────────

    [Fact]
    public async Task Seed_PumpMode_OverridesRapidInsulinSourceToNone()
    {
        var created = new List<InventoryItemEntity>();
        _repo.Setup(r => r.GetItemsAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => created.ToList());
        _repo.Setup(r => r.CreateItemAsync(It.IsAny<InventoryItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItemEntity e, CancellationToken _) =>
            {
                e.Id = Guid.NewGuid();
                created.Add(e);
                return e;
            });

        var svc = NewService();
        await svc.SeedFromSelectionAsync(new InventorySeedRequest(
            TherapyMode.Pump,
            CgmKeys: ["dexcom-g7"],
            PumpKey: "omnipod-5",
            RapidInsulinKey: "humalog",
            BasalInsulinKey: null));

        var rapid = created.Single(e => e.Kind == InventoryKind.Insulin);
        rapid.AutoConsumeSource.Should().Be(InventoryAutoConsumeSource.None,
            "pump rapid insulin drains via the linked Pod/Reservoir change, not via Bolus");
    }

    [Fact]
    public async Task Seed_MdiMode_RapidInsulinKeepsBolusSource()
    {
        var created = new List<InventoryItemEntity>();
        _repo.Setup(r => r.GetItemsAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => created.ToList());
        _repo.Setup(r => r.CreateItemAsync(It.IsAny<InventoryItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItemEntity e, CancellationToken _) =>
            {
                e.Id = Guid.NewGuid();
                created.Add(e);
                return e;
            });

        var svc = NewService();
        await svc.SeedFromSelectionAsync(new InventorySeedRequest(
            TherapyMode.Mdi,
            CgmKeys: ["libre-3"],
            PumpKey: null,
            RapidInsulinKey: "novolog",
            BasalInsulinKey: "tresiba"));

        var rapid = created.Single(e => e.Kind == InventoryKind.Insulin && e.Name.Contains("NovoLog"));
        rapid.AutoConsumeSource.Should().Be(InventoryAutoConsumeSource.Bolus,
            "MDI rapid insulin drains directly on every bolus");
    }

    [Fact]
    public async Task Seed_MdiMode_SeedsBasalInsulinWhenSpecified()
    {
        var created = new List<InventoryItemEntity>();
        _repo.Setup(r => r.GetItemsAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => created.ToList());
        _repo.Setup(r => r.CreateItemAsync(It.IsAny<InventoryItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItemEntity e, CancellationToken _) =>
            {
                e.Id = Guid.NewGuid();
                created.Add(e);
                return e;
            });

        var svc = NewService();
        await svc.SeedFromSelectionAsync(new InventorySeedRequest(
            TherapyMode.Mdi,
            CgmKeys: [],
            PumpKey: null,
            RapidInsulinKey: null,
            BasalInsulinKey: "tresiba"));

        var basal = created.Single(e => e.Kind == InventoryKind.Insulin);
        basal.AutoConsumeSource.Should().Be(InventoryAutoConsumeSource.BasalInjection);
        basal.Name.Should().Contain("Tresiba");
    }

    [Fact]
    public async Task Seed_PumpMode_IgnoresBasalInsulinKey()
    {
        var created = new List<InventoryItemEntity>();
        _repo.Setup(r => r.GetItemsAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => created.ToList());
        _repo.Setup(r => r.CreateItemAsync(It.IsAny<InventoryItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItemEntity e, CancellationToken _) =>
            {
                e.Id = Guid.NewGuid();
                created.Add(e);
                return e;
            });

        var svc = NewService();
        await svc.SeedFromSelectionAsync(new InventorySeedRequest(
            TherapyMode.Pump,
            CgmKeys: [],
            PumpKey: "tslim-x2",
            RapidInsulinKey: "humalog",
            BasalInsulinKey: "tresiba"));

        created.Should().NotContain(e =>
            e.Kind == InventoryKind.Insulin && e.AutoConsumeSource == InventoryAutoConsumeSource.BasalInjection,
            "pump mode never seeds long-acting basal insulin even if a key is passed");
    }

    [Fact]
    public async Task Seed_AlwaysSeedsGenericSupplies()
    {
        var created = new List<InventoryItemEntity>();
        _repo.Setup(r => r.GetItemsAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => created.ToList());
        _repo.Setup(r => r.CreateItemAsync(It.IsAny<InventoryItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItemEntity e, CancellationToken _) =>
            {
                e.Id = Guid.NewGuid();
                created.Add(e);
                return e;
            });

        var svc = NewService();
        await svc.SeedFromSelectionAsync(new InventorySeedRequest(
            TherapyMode.Mdi,
            CgmKeys: [],
            PumpKey: null,
            RapidInsulinKey: null,
            BasalInsulinKey: null));

        created.Should().Contain(e => e.Kind == InventoryKind.TestStrip);
        created.Should().Contain(e => e.Kind == InventoryKind.Lancet);
        created.Should().Contain(e => e.Kind == InventoryKind.Glucagon);
        created.Should().Contain(e => e.Kind == InventoryKind.FastCarbs);
    }

    [Fact]
    public async Task Seed_IsIdempotent_PerKindAndName()
    {
        var created = new List<InventoryItemEntity>();
        _repo.Setup(r => r.GetItemsAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => created.ToList());
        _repo.Setup(r => r.CreateItemAsync(It.IsAny<InventoryItemEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InventoryItemEntity e, CancellationToken _) =>
            {
                e.Id = Guid.NewGuid();
                created.Add(e);
                return e;
            });

        var svc = NewService();
        var request = new InventorySeedRequest(
            TherapyMode.Mdi,
            CgmKeys: ["libre-3"],
            PumpKey: null,
            RapidInsulinKey: "novolog",
            BasalInsulinKey: "tresiba");

        await svc.SeedFromSelectionAsync(request);
        var firstRunCount = created.Count;
        await svc.SeedFromSelectionAsync(request);

        created.Count.Should().Be(firstRunCount,
            "re-running seed must not duplicate items that already exist");
    }

    // ── Run-out projection ─────────────────────────────────────────────

    [Fact]
    public void ProjectRunOut_WearTimeStrategy_ProjectsForward()
    {
        var item = new InventoryItemEntity
        {
            Id = Guid.NewGuid(),
            Kind = InventoryKind.CgmSensor,
            WearDays = 10,
            Batches = new List<InventoryBatchEntity>
            {
                new() { Id = Guid.NewGuid(), RemainingQuantity = 4, StorageState = InventoryStorageState.Normal }
            }
        };
        var now = new DateTime(2026, 5, 22, 12, 0, 0, DateTimeKind.Utc);

        var (runOut, source) = InventoryService.ProjectRunOut(item, 4, null, Array.Empty<InventoryTransactionEntity>(), now);

        source.Should().Be(RunOutProjectionSource.WearTime);
        runOut.Should().Be(now.AddDays(40));
    }

    [Fact]
    public void ProjectRunOut_LinkedItem_PodsRunOutFirst()
    {
        // 3 pods × 200u-per-change = 600u of drain. Insulin has 1200u, so pods run out first.
        var insulin = new InventoryItemEntity
        {
            Id = Guid.NewGuid(),
            Kind = InventoryKind.Insulin
        };
        var pod = new InventoryItemEntity
        {
            Id = Guid.NewGuid(),
            Kind = InventoryKind.Pod,
            WearDays = 3,
            LinkedInsulinItemId = insulin.Id,
            LinkedInsulinUnitsPerUse = 200,
            Batches = new List<InventoryBatchEntity>
            {
                new() { Id = Guid.NewGuid(), RemainingQuantity = 3, StorageState = InventoryStorageState.Normal }
            }
        };
        var itemsById = new Dictionary<Guid, InventoryItemEntity> { [pod.Id] = pod, [insulin.Id] = insulin };
        var now = new DateTime(2026, 5, 22, 12, 0, 0, DateTimeKind.Utc);

        var (runOut, source) = InventoryService.ProjectRunOut(insulin, 1200, itemsById, Array.Empty<InventoryTransactionEntity>(), now);

        source.Should().Be(RunOutProjectionSource.LinkedItem);
        runOut.Should().Be(now.AddDays(9), "3 pods × 3 days = 9 days; insulin would last longer so pods bound it");
    }

    [Fact]
    public void ProjectRunOut_LinkedItem_InsulinRunsOutFirst()
    {
        // 5 pods × 200u = 1000u of drain possible. But only 400u of insulin → 2 pod changes worth.
        var insulin = new InventoryItemEntity { Id = Guid.NewGuid(), Kind = InventoryKind.Insulin };
        var pod = new InventoryItemEntity
        {
            Id = Guid.NewGuid(),
            Kind = InventoryKind.Pod,
            WearDays = 3,
            LinkedInsulinItemId = insulin.Id,
            LinkedInsulinUnitsPerUse = 200,
            Batches = new List<InventoryBatchEntity>
            {
                new() { Id = Guid.NewGuid(), RemainingQuantity = 5, StorageState = InventoryStorageState.Normal }
            }
        };
        var itemsById = new Dictionary<Guid, InventoryItemEntity> { [pod.Id] = pod, [insulin.Id] = insulin };
        var now = new DateTime(2026, 5, 22, 12, 0, 0, DateTimeKind.Utc);

        var (runOut, source) = InventoryService.ProjectRunOut(insulin, 400, itemsById, Array.Empty<InventoryTransactionEntity>(), now);

        source.Should().Be(RunOutProjectionSource.LinkedItem);
        runOut.Should().Be(now.AddDays(6), "400u / 200u-per-change × 3 days/change = 6 days");
    }

    [Fact]
    public void ProjectRunOut_HistoricalRate_AveragesOverWindow()
    {
        // Used 28 strips in 14 days → 2/day. 56 strips remaining → 28 days runway.
        var itemId = Guid.NewGuid();
        var item = new InventoryItemEntity
        {
            Id = itemId,
            Kind = InventoryKind.TestStrip,
            // No WearDays — strips are consumption-rate-based
            Batches = new List<InventoryBatchEntity>
            {
                new() { Id = Guid.NewGuid(), RemainingQuantity = 56, StorageState = InventoryStorageState.Normal }
            }
        };
        var now = new DateTime(2026, 5, 22, 12, 0, 0, DateTimeKind.Utc);
        var txns = new List<InventoryTransactionEntity>
        {
            new() { InventoryItemId = itemId, Type = InventoryTransactionType.ManualConsume, QuantityDelta = -10, CreatedAt = now.AddDays(-10) },
            new() { InventoryItemId = itemId, Type = InventoryTransactionType.ManualConsume, QuantityDelta = -8, CreatedAt = now.AddDays(-5) },
            new() { InventoryItemId = itemId, Type = InventoryTransactionType.ManualConsume, QuantityDelta = -10, CreatedAt = now.AddDays(-2) }
        };

        var (runOut, source) = InventoryService.ProjectRunOut(item, 56, null, txns, now);

        source.Should().Be(RunOutProjectionSource.HistoricalRate);
        runOut.Should().Be(now.AddDays(28));
    }

    [Fact]
    public void ProjectRunOut_HistoricalRate_InsufficientHistoryReturnsNull()
    {
        var itemId = Guid.NewGuid();
        var item = new InventoryItemEntity { Id = itemId, Kind = InventoryKind.TestStrip };
        var now = new DateTime(2026, 5, 22, 12, 0, 0, DateTimeKind.Utc);
        var txns = new List<InventoryTransactionEntity>
        {
            new() { InventoryItemId = itemId, Type = InventoryTransactionType.ManualConsume, QuantityDelta = -5, CreatedAt = now.AddDays(-3) }
        };

        var (runOut, source) = InventoryService.ProjectRunOut(item, 50, null, txns, now);

        runOut.Should().BeNull();
        source.Should().BeNull();
    }

    [Fact]
    public void ProjectRunOut_ZeroUsable_ReturnsNull()
    {
        var item = new InventoryItemEntity { Id = Guid.NewGuid(), Kind = InventoryKind.CgmSensor, WearDays = 10 };

        var (runOut, source) = InventoryService.ProjectRunOut(item, 0, null, Array.Empty<InventoryTransactionEntity>(), DateTime.UtcNow);

        runOut.Should().BeNull();
        source.Should().BeNull();
    }

    [Fact]
    public void ProjectRunOut_NoWearDaysNoHistory_ReturnsNull()
    {
        // Glucagon: no wear days, no consumption history → "cannot estimate".
        var item = new InventoryItemEntity { Id = Guid.NewGuid(), Kind = InventoryKind.Glucagon };

        var (runOut, source) = InventoryService.ProjectRunOut(item, 1, null, Array.Empty<InventoryTransactionEntity>(), DateTime.UtcNow);

        runOut.Should().BeNull();
        source.Should().BeNull();
    }

    [Fact]
    public void ProjectRunOut_LinkedItemTakesPriorityOverWearTime()
    {
        // Edge case: an insulin item ALSO has WearDays set (e.g., misconfigured).
        // LinkedItem strategy should win because it's the more semantically correct projection for pump insulin.
        var insulin = new InventoryItemEntity { Id = Guid.NewGuid(), Kind = InventoryKind.Insulin, WearDays = 5 };
        var pod = new InventoryItemEntity
        {
            Id = Guid.NewGuid(),
            Kind = InventoryKind.Pod,
            WearDays = 3,
            LinkedInsulinItemId = insulin.Id,
            LinkedInsulinUnitsPerUse = 200,
            Batches = new List<InventoryBatchEntity>
            {
                new() { Id = Guid.NewGuid(), RemainingQuantity = 2, StorageState = InventoryStorageState.Normal }
            }
        };
        var itemsById = new Dictionary<Guid, InventoryItemEntity> { [pod.Id] = pod, [insulin.Id] = insulin };
        var now = new DateTime(2026, 5, 22, 12, 0, 0, DateTimeKind.Utc);

        var (_, source) = InventoryService.ProjectRunOut(insulin, 600, itemsById, Array.Empty<InventoryTransactionEntity>(), now);

        source.Should().Be(RunOutProjectionSource.LinkedItem);
    }
}
