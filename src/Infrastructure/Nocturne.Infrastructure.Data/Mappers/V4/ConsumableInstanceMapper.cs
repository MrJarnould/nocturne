using Nocturne.Core.Models.V4;
using Nocturne.Infrastructure.Data.Entities.V4;

namespace Nocturne.Infrastructure.Data.Mappers.V4;

/// <summary>
/// Mapper for <see cref="ConsumableInstance"/> ↔ <see cref="ConsumableInstanceEntity"/>.
/// </summary>
public static class ConsumableInstanceMapper
{
    public static ConsumableInstanceEntity ToEntity(ConsumableInstance model)
    {
        return new ConsumableInstanceEntity
        {
            Id = model.Id == Guid.Empty ? Guid.CreateVersion7() : model.Id,
            SourceDeviceEventId = model.SourceDeviceEventId,
            ConsumableCatalogId = model.ConsumableCatalogId,
            Kind = model.Kind.ToString(),
            PatientDeviceId = model.PatientDeviceId,
            DeviceId = model.DeviceId,
            InventoryItemId = model.InventoryItemId,
            InventoryBatchId = model.InventoryBatchId,
            SerialNumber = model.SerialNumber,
            InsertionSite = model.InsertionSite,
            StartedAt = DateTime.SpecifyKind(model.StartedAt, DateTimeKind.Utc),
            EndedAt = model.EndedAt is { } ended ? DateTime.SpecifyKind(ended, DateTimeKind.Utc) : null,
            EndReason = model.EndReason?.ToString(),
            Notes = model.Notes,
            SnapshotWearDays = model.SnapshotWearDays,
            SnapshotReservoirCapacity = model.SnapshotReservoirCapacity,
            FilledUnits = model.FilledUnits,
            ResidualUnits = model.ResidualUnits,
            SysCreatedAt = DateTime.UtcNow,
            SysUpdatedAt = DateTime.UtcNow,
        };
    }

    public static ConsumableInstance ToDomainModel(ConsumableInstanceEntity entity)
    {
        return new ConsumableInstance
        {
            Id = entity.Id,
            SourceDeviceEventId = entity.SourceDeviceEventId,
            ConsumableCatalogId = entity.ConsumableCatalogId,
            Kind = Enum.TryParse<ConsumableKind>(entity.Kind, ignoreCase: true, out var kind)
                ? kind
                : ConsumableKind.CgmSensor, // safe default; defensive against future enum drift
            PatientDeviceId = entity.PatientDeviceId,
            DeviceId = entity.DeviceId,
            InventoryItemId = entity.InventoryItemId,
            InventoryBatchId = entity.InventoryBatchId,
            SerialNumber = entity.SerialNumber,
            InsertionSite = entity.InsertionSite,
            StartedAt = DateTime.SpecifyKind(entity.StartedAt, DateTimeKind.Utc),
            EndedAt = entity.EndedAt is { } ended ? DateTime.SpecifyKind(ended, DateTimeKind.Utc) : null,
            EndReason = entity.EndReason is not null
                && Enum.TryParse<ConsumableInstanceEndReason>(entity.EndReason, ignoreCase: true, out var reason)
                ? reason : null,
            Notes = entity.Notes,
            SnapshotWearDays = entity.SnapshotWearDays,
            SnapshotReservoirCapacity = entity.SnapshotReservoirCapacity,
            FilledUnits = entity.FilledUnits,
            ResidualUnits = entity.ResidualUnits,
            CreatedAt = entity.SysCreatedAt,
            ModifiedAt = entity.SysUpdatedAt,
        };
    }

    public static void UpdateEntity(ConsumableInstanceEntity entity, ConsumableInstance model)
    {
        // SourceDeviceEventId is set at creation only and never updated.
        entity.ConsumableCatalogId = model.ConsumableCatalogId;
        entity.Kind = model.Kind.ToString();
        entity.PatientDeviceId = model.PatientDeviceId;
        entity.DeviceId = model.DeviceId;
        entity.InventoryItemId = model.InventoryItemId;
        entity.InventoryBatchId = model.InventoryBatchId;
        entity.SerialNumber = model.SerialNumber;
        entity.InsertionSite = model.InsertionSite;
        entity.StartedAt = DateTime.SpecifyKind(model.StartedAt, DateTimeKind.Utc);
        entity.EndedAt = model.EndedAt is { } ended ? DateTime.SpecifyKind(ended, DateTimeKind.Utc) : null;
        entity.EndReason = model.EndReason?.ToString();
        entity.Notes = model.Notes;
        entity.SnapshotWearDays = model.SnapshotWearDays;
        entity.SnapshotReservoirCapacity = model.SnapshotReservoirCapacity;
        entity.FilledUnits = model.FilledUnits;
        entity.ResidualUnits = model.ResidualUnits;
        entity.SysUpdatedAt = DateTime.UtcNow;
    }
}
