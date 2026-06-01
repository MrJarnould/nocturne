using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Nocturne.API.Services.V4;
using Nocturne.Core.Contracts.V4.Repositories;
using Nocturne.Core.Models;
using Nocturne.Core.Models.V4;
using Xunit;

namespace Nocturne.API.Tests.Services.V4;

/// <summary>
/// Mock-only tests for <see cref="ConsumableInstanceService.HandleDeviceEventAsync"/>:
/// the hook is pure orchestration over <see cref="IConsumableInstanceRepository"/> and
/// <see cref="IPatientDeviceRepository"/>, with all branching decisions coming out of
/// the static <see cref="ConsumableCatalog"/> / <see cref="DeviceCatalog"/> lookups.
/// </summary>
public class ConsumableInstanceServiceTests
{
    private readonly Mock<IConsumableInstanceRepository> _instances = new(MockBehavior.Strict);
    private readonly Mock<IPatientDeviceRepository> _patientDevices = new(MockBehavior.Strict);
    private readonly ConsumableInstanceService _service;

    public ConsumableInstanceServiceTests()
    {
        _service = new ConsumableInstanceService(
            _instances.Object,
            _patientDevices.Object,
            NullLogger<ConsumableInstanceService>.Instance);
    }

    // ── Helpers ─────────────────────────────────────────────────────────

    /// <summary>
    /// Pick the first device-catalog entry that satisfies the predicate. We do
    /// this rather than hard-coding ids so the tests don't break when the
    /// catalog is reseeded.
    /// </summary>
    private static DeviceCatalogEntry RequireDevice(Func<DeviceCatalogEntry, bool> pred)
        => DeviceCatalog.GetAll().FirstOrDefault(pred)
           ?? throw new InvalidOperationException("Test catalog precondition not met");

    private static ConsumableCatalogEntry RequireConsumable(string deviceCatalogId, ConsumableKind kind)
        => ConsumableCatalog.GetForDevice(deviceCatalogId).FirstOrDefault(c => c.Kind == kind)
           ?? throw new InvalidOperationException(
               $"No {kind} consumable in catalog for device {deviceCatalogId}; test precondition not met");

    private static DeviceEvent EventOf(DeviceEventType type, DateTime? at = null)
        => new()
        {
            Id = Guid.CreateVersion7(),
            EventType = type,
            Timestamp = at ?? DateTime.UtcNow,
            DeviceId = Guid.CreateVersion7(),
        };

    private static PatientDevice DeviceOf(DeviceCategory category, string catalogId)
        => new()
        {
            Id = Guid.CreateVersion7(),
            DeviceCategory = category,
            CatalogId = catalogId,
        };

    private void ExpectNoOpenInstance(ConsumableKind kind) =>
        _instances.Setup(r => r.GetOpenByKindAsync(kind, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ConsumableInstance?)null);

    private void ExpectNoIdempotencyHit() =>
        _instances.Setup(r => r.GetBySourceDeviceEventIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ConsumableInstance?)null);

    private void ExpectCurrentDevices(params PatientDevice[] devices) =>
        _patientDevices.Setup(r => r.GetCurrentAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(devices);

    // ── Idempotency ─────────────────────────────────────────────────────

    [Fact]
    public async Task HandleDeviceEventAsync_NoOps_WhenSameEventReplayed()
    {
        var evt = EventOf(DeviceEventType.SensorStart);
        var existing = new ConsumableInstance
        {
            Id = Guid.CreateVersion7(),
            SourceDeviceEventId = evt.Id,
            Kind = ConsumableKind.CgmSensor,
            StartedAt = evt.Timestamp,
        };

        _instances.Setup(r => r.GetBySourceDeviceEventIdAsync(evt.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        await _service.HandleDeviceEventAsync(evt);

        // No follow-up writes — no GetCurrent, no Create, no Update.
        _patientDevices.Verify(r => r.GetCurrentAsync(It.IsAny<CancellationToken>()), Times.Never);
        _instances.Verify(r => r.CreateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandleDeviceEventAsync_NoOps_OnUnsupportedEventType()
    {
        var evt = EventOf(DeviceEventType.PumpBatteryChange);

        // No repository calls beyond the event-type guard — verify by leaving
        // mocks strict and asserting completion without setup.
        await _service.HandleDeviceEventAsync(evt);

        _instances.VerifyNoOtherCalls();
        _patientDevices.VerifyNoOtherCalls();
    }

    // ── SensorStart → CgmSensor open + close prior ──────────────────────

    [Fact]
    public async Task HandleDeviceEventAsync_OpensSensorInstance_OnSensorStartWithCgmDevice()
    {
        var cgmDevice = RequireDevice(d => d.Cgm is not null);
        var sensor = RequireConsumable(cgmDevice.Id, ConsumableKind.CgmSensor);
        var patientCgm = DeviceOf(DeviceCategory.CGM, cgmDevice.Id);
        var evt = EventOf(DeviceEventType.SensorStart);

        ExpectNoIdempotencyHit();
        ExpectCurrentDevices(patientCgm);
        ExpectNoOpenInstance(ConsumableKind.CgmSensor);

        ConsumableInstance? created = null;
        _instances.Setup(r => r.CreateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()))
            .Callback<ConsumableInstance, CancellationToken>((i, _) => created = i)
            .ReturnsAsync((ConsumableInstance i, CancellationToken _) => i);

        await _service.HandleDeviceEventAsync(evt);

        created.Should().NotBeNull();
        created!.Kind.Should().Be(ConsumableKind.CgmSensor);
        created.SourceDeviceEventId.Should().Be(evt.Id);
        created.ConsumableCatalogId.Should().Be(sensor.Id);
        created.PatientDeviceId.Should().Be(patientCgm.Id);
        created.StartedAt.Should().Be(DateTime.SpecifyKind(evt.Timestamp, DateTimeKind.Utc));
        created.SnapshotWearDays.Should().Be(sensor.WearDays);
        // Sensors have no reservoir.
        created.SnapshotReservoirCapacity.Should().BeNull();
    }

    [Fact]
    public async Task HandleDeviceEventAsync_ClosesPriorOpen_BeforeOpeningNew()
    {
        var cgmDevice = RequireDevice(d => d.Cgm is not null);
        var patientCgm = DeviceOf(DeviceCategory.CGM, cgmDevice.Id);
        var evt = EventOf(DeviceEventType.SensorStart, DateTime.UtcNow);

        var prior = new ConsumableInstance
        {
            Id = Guid.CreateVersion7(),
            Kind = ConsumableKind.CgmSensor,
            StartedAt = evt.Timestamp.AddDays(-10),
        };

        ExpectNoIdempotencyHit();
        ExpectCurrentDevices(patientCgm);
        _instances.Setup(r => r.GetOpenByKindAsync(ConsumableKind.CgmSensor, It.IsAny<CancellationToken>()))
            .ReturnsAsync(prior);
        ConsumableInstance? updated = null;
        _instances.Setup(r => r.UpdateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()))
            .Callback<ConsumableInstance, CancellationToken>((i, _) => updated = i)
            .ReturnsAsync((ConsumableInstance i, CancellationToken _) => i);
        _instances.Setup(r => r.CreateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ConsumableInstance i, CancellationToken _) => i);

        await _service.HandleDeviceEventAsync(evt);

        updated.Should().NotBeNull();
        updated!.Id.Should().Be(prior.Id);
        updated.EndedAt.Should().Be(DateTime.SpecifyKind(evt.Timestamp, DateTimeKind.Utc));
        updated.EndReason.Should().Be(ConsumableInstanceEndReason.Planned);
    }

    [Fact]
    public async Task HandleDeviceEventAsync_SkipsSensorStart_WhenNoCgmDevice()
    {
        var evt = EventOf(DeviceEventType.SensorStart);

        ExpectNoIdempotencyHit();
        ExpectCurrentDevices(); // empty — no CGM

        await _service.HandleDeviceEventAsync(evt);

        _instances.Verify(
            r => r.CreateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    // ── SiteChange → Pod (tubeless) vs InfusionSet (tubed) ──────────────

    [Fact]
    public async Task HandleDeviceEventAsync_OpensPod_OnSiteChangeWithTubelessPump()
    {
        var tubeless = RequireDevice(d => d.Pump is not null && d.Pump.IsTubeless);
        var pod = RequireConsumable(tubeless.Id, ConsumableKind.Pod);
        var patientPump = DeviceOf(DeviceCategory.InsulinPump, tubeless.Id);
        var evt = EventOf(DeviceEventType.SiteChange);

        ExpectNoIdempotencyHit();
        ExpectCurrentDevices(patientPump);
        ExpectNoOpenInstance(ConsumableKind.Pod);

        ConsumableInstance? created = null;
        _instances.Setup(r => r.CreateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()))
            .Callback<ConsumableInstance, CancellationToken>((i, _) => created = i)
            .ReturnsAsync((ConsumableInstance i, CancellationToken _) => i);

        await _service.HandleDeviceEventAsync(evt);

        created.Should().NotBeNull();
        created!.Kind.Should().Be(ConsumableKind.Pod);
        created.ConsumableCatalogId.Should().Be(pod.Id);
        // Tubeless pumps still have a reservoir capacity snapshot on the pod itself.
        created.SnapshotReservoirCapacity.Should().Be(tubeless.Pump!.ReservoirCapacityUnits);
    }

    [Fact]
    public async Task HandleDeviceEventAsync_OpensInfusionSet_OnSiteChangeWithTubedPump()
    {
        var tubed = RequireDevice(d => d.Pump is not null && !d.Pump.IsTubeless);
        var set = RequireConsumable(tubed.Id, ConsumableKind.InfusionSet);
        var patientPump = DeviceOf(DeviceCategory.InsulinPump, tubed.Id);
        var evt = EventOf(DeviceEventType.SiteChange);

        ExpectNoIdempotencyHit();
        ExpectCurrentDevices(patientPump);
        ExpectNoOpenInstance(ConsumableKind.InfusionSet);

        ConsumableInstance? created = null;
        _instances.Setup(r => r.CreateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()))
            .Callback<ConsumableInstance, CancellationToken>((i, _) => created = i)
            .ReturnsAsync((ConsumableInstance i, CancellationToken _) => i);

        await _service.HandleDeviceEventAsync(evt);

        created.Should().NotBeNull();
        created!.Kind.Should().Be(ConsumableKind.InfusionSet);
        created.ConsumableCatalogId.Should().Be(set.Id);
    }

    // ── Update editable subset ──────────────────────────────────────────

    [Fact]
    public async Task UpdateAsync_PatchesOnlyEditableFields()
    {
        var id = Guid.CreateVersion7();
        var current = new ConsumableInstance
        {
            Id = id,
            ConsumableCatalogId = "dexcom-g7-sensor",
            Kind = ConsumableKind.CgmSensor,
            StartedAt = DateTime.UtcNow.AddDays(-3),
            InsertionSite = "upper-arm-left",
            Notes = null,
        };
        _instances.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(current);

        ConsumableInstance? saved = null;
        _instances.Setup(r => r.UpdateAsync(It.IsAny<ConsumableInstance>(), It.IsAny<CancellationToken>()))
            .Callback<ConsumableInstance, CancellationToken>((i, _) => saved = i)
            .ReturnsAsync((ConsumableInstance i, CancellationToken _) => i);

        var patch = new Nocturne.Core.Contracts.V4.ConsumableInstanceEditRequest(
            InsertionSite: "lower-back",
            SerialNumber: null,
            Notes: "first prick painful",
            EndedAt: null,
            EndReason: null,
            ResidualUnits: null);

        await _service.UpdateAsync(id, patch);

        saved.Should().NotBeNull();
        saved!.InsertionSite.Should().Be("lower-back");
        saved.Notes.Should().Be("first prick painful");
        // Untouched fields remain.
        saved.Kind.Should().Be(ConsumableKind.CgmSensor);
        saved.ConsumableCatalogId.Should().Be("dexcom-g7-sensor");
    }
}
