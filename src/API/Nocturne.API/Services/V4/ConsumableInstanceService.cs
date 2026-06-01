using Microsoft.Extensions.Logging;
using Nocturne.Core.Contracts.V4;
using Nocturne.Core.Contracts.V4.Repositories;
using Nocturne.Core.Models;
using Nocturne.Core.Models.V4;

namespace Nocturne.API.Services.V4;

/// <inheritdoc cref="IConsumableInstanceService"/>
public class ConsumableInstanceService : IConsumableInstanceService
{
    private readonly IConsumableInstanceRepository _repo;
    private readonly IPatientDeviceRepository _patientDevices;
    private readonly ILogger<ConsumableInstanceService> _logger;

    public ConsumableInstanceService(
        IConsumableInstanceRepository repo,
        IPatientDeviceRepository patientDevices,
        ILogger<ConsumableInstanceService> logger)
    {
        _repo = repo;
        _patientDevices = patientDevices;
        _logger = logger;
    }

    public async Task HandleDeviceEventAsync(DeviceEvent deviceEvent, CancellationToken ct = default)
    {
        if (deviceEvent.Id == Guid.Empty)
        {
            _logger.LogDebug("ConsumableInstance hook called with an unsaved DeviceEvent (Id == Guid.Empty); skipping");
            return;
        }

        // Only SensorStart and SiteChange open instances in Phase 1.
        if (deviceEvent.EventType is not (DeviceEventType.SensorStart or DeviceEventType.SiteChange))
            return;

        // Idempotency: if this event already opened an instance, do nothing.
        var existing = await _repo.GetBySourceDeviceEventIdAsync(deviceEvent.Id, ct);
        if (existing is not null)
        {
            _logger.LogDebug(
                "ConsumableInstance hook: event {EventId} ({EventType}) already opened instance {InstanceId}; skipping",
                deviceEvent.Id, deviceEvent.EventType, existing.Id);
            return;
        }

        // Resolve the catalog entry the event implies, based on the patient's
        // current device. Without a current device we can't honestly say
        // what brand of sensor / pod the patient is wearing, so we skip.
        var resolution = await ResolveConsumableForEventAsync(deviceEvent, ct);
        if (resolution is null)
        {
            _logger.LogInformation(
                "ConsumableInstance hook: cannot resolve a consumable for {EventType} on tenant — no current device of the matching category",
                deviceEvent.EventType);
            return;
        }

        var (kind, catalogEntry, patientDeviceId) = resolution.Value;

        // Close the previous open instance of this kind, if any.
        var previousOpen = await _repo.GetOpenByKindAsync(kind, ct);
        if (previousOpen is not null)
        {
            previousOpen.EndedAt = DateTime.SpecifyKind(deviceEvent.Timestamp, DateTimeKind.Utc);
            previousOpen.EndReason = ConsumableInstanceEndReason.Planned;
            await _repo.UpdateAsync(previousOpen, ct);
        }

        // Open the new instance. SnapshotReservoirCapacity stays null for
        // CGM sensors (only pumps have a reservoir capacity).
        var pumpEntry = catalogEntry.DeviceCatalogId is null
            ? null
            : DeviceCatalog.GetById(catalogEntry.DeviceCatalogId);
        var snapshotReservoir = pumpEntry?.Pump?.ReservoirCapacityUnits;

        var instance = new ConsumableInstance
        {
            SourceDeviceEventId = deviceEvent.Id,
            ConsumableCatalogId = catalogEntry.Id,
            Kind = kind,
            PatientDeviceId = patientDeviceId,
            DeviceId = deviceEvent.DeviceId,
            StartedAt = DateTime.SpecifyKind(deviceEvent.Timestamp, DateTimeKind.Utc),
            SnapshotWearDays = catalogEntry.WearDays,
            SnapshotReservoirCapacity = snapshotReservoir,
        };

        var created = await _repo.CreateAsync(instance, ct);
        _logger.LogDebug(
            "Opened ConsumableInstance {InstanceId} ({Kind}, catalog {CatalogId}) from DeviceEvent {EventId}",
            created.Id, kind, catalogEntry.Id, deviceEvent.Id);
    }

    /// <summary>
    /// Maps a DeviceEvent → the (kind, catalog entry, patient device id) it
    /// should open, using the patient's current device declarations to decide
    /// brand and consumable kind. Returns null when no resolution is possible.
    /// </summary>
    private async Task<(ConsumableKind kind, ConsumableCatalogEntry catalogEntry, Guid? patientDeviceId)?>
        ResolveConsumableForEventAsync(DeviceEvent deviceEvent, CancellationToken ct)
    {
        var currentDevices = (await _patientDevices.GetCurrentAsync(ct)).ToList();
        if (currentDevices.Count == 0)
            return null;

        return deviceEvent.EventType switch
        {
            DeviceEventType.SensorStart => ResolveSensor(currentDevices),
            DeviceEventType.SiteChange => ResolveInfusionOrPod(currentDevices),
            _ => null,
        };
    }

    private static (ConsumableKind, ConsumableCatalogEntry, Guid?)? ResolveSensor(IReadOnlyList<PatientDevice> currentDevices)
    {
        var cgm = currentDevices.FirstOrDefault(d => d.DeviceCategory == DeviceCategory.CGM && d.CatalogId is not null);
        if (cgm is null) return null;

        var sensor = ConsumableCatalog.GetForDevice(cgm.CatalogId!)
            .FirstOrDefault(c => c.Kind == ConsumableKind.CgmSensor);
        return sensor is null ? null : (ConsumableKind.CgmSensor, sensor, cgm.Id);
    }

    private static (ConsumableKind, ConsumableCatalogEntry, Guid?)? ResolveInfusionOrPod(IReadOnlyList<PatientDevice> currentDevices)
    {
        var pump = currentDevices.FirstOrDefault(d => d.DeviceCategory == DeviceCategory.InsulinPump && d.CatalogId is not null);
        if (pump is null) return null;

        var pumpEntry = DeviceCatalog.GetById(pump.CatalogId!);
        if (pumpEntry?.Pump is null) return null;

        var consumables = ConsumableCatalog.GetForDevice(pump.CatalogId!);
        if (pumpEntry.Pump.IsTubeless)
        {
            var pod = consumables.FirstOrDefault(c => c.Kind == ConsumableKind.Pod);
            return pod is null ? null : (ConsumableKind.Pod, pod, pump.Id);
        }

        var infusionSet = consumables.FirstOrDefault(c => c.Kind == ConsumableKind.InfusionSet);
        return infusionSet is null ? null : (ConsumableKind.InfusionSet, infusionSet, pump.Id);
    }

    public Task<IReadOnlyList<ConsumableInstance>> GetActiveAsync(CancellationToken ct = default)
        => _repo.GetAllOpenAsync(ct);

    public Task<IReadOnlyList<ConsumableInstance>> GetRecentClosedAsync(int limit, CancellationToken ct = default)
        => _repo.GetRecentClosedAsync(limit, ct);

    public Task<ConsumableInstance?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public async Task<ConsumableInstance?> UpdateAsync(Guid id, ConsumableInstanceEditRequest patch, CancellationToken ct = default)
    {
        var current = await _repo.GetByIdAsync(id, ct);
        if (current is null) return null;

        // Only the editable fields are honoured; identity / catalog / inventory
        // FKs are immutable from this surface.
        if (patch.InsertionSite is not null) current.InsertionSite = patch.InsertionSite;
        if (patch.SerialNumber is not null) current.SerialNumber = patch.SerialNumber;
        if (patch.Notes is not null) current.Notes = patch.Notes;
        if (patch.EndedAt is not null) current.EndedAt = DateTime.SpecifyKind(patch.EndedAt.Value, DateTimeKind.Utc);
        if (patch.EndReason is not null) current.EndReason = patch.EndReason;
        if (patch.ResidualUnits is not null) current.ResidualUnits = patch.ResidualUnits;

        return await _repo.UpdateAsync(current, ct);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => _repo.DeleteAsync(id, ct);
}
