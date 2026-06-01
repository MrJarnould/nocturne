using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nocturne.Infrastructure.Data.Entities.V4;

/// <summary>
/// PostgreSQL entity for <see cref="Nocturne.Core.Models.V4.ConsumableInstance"/>.
/// Tenant-scoped + soft-deletable. RLS is enforced via the
/// <c>tenant_isolation</c> policy on <c>consumable_instances</c>.
/// </summary>
[Table("consumable_instances")]
public class ConsumableInstanceEntity : ITenantScoped, ISoftDeletable
{
    [Column("tenant_id")]
    public Guid TenantId { get; set; }

    /// <summary>UUID v7 primary key.</summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// FK to <c>device_events.id</c>. Used as the idempotency key by the
    /// open-on-event hook so a resync doesn't open duplicate instances.
    /// Null when the instance was opened by a manual UI action.
    /// </summary>
    [Column("source_device_event_id")]
    public Guid? SourceDeviceEventId { get; set; }

    /// <summary>
    /// FK to <see cref="Nocturne.Core.Models.V4.ConsumableCatalogEntry.Id"/>.
    /// Stored as a string because the consumable catalog itself is a static
    /// in-code list keyed by kebab-case IDs, not a DB table.
    /// </summary>
    [Column("consumable_catalog_id")]
    [MaxLength(128)]
    public string ConsumableCatalogId { get; set; } = string.Empty;

    /// <summary>
    /// Denormalised from the catalog entry so the open-instance lookup can
    /// filter by kind without joining the catalog at runtime.
    /// </summary>
    [Column("kind")]
    [MaxLength(32)]
    public string Kind { get; set; } = string.Empty;

    /// <summary>FK to <see cref="PatientDeviceEntity"/>. Null when not yet linked.</summary>
    [Column("patient_device_id")]
    public Guid? PatientDeviceId { get; set; }

    /// <summary>FK to canonical <c>devices.id</c>. Null when not yet linked.</summary>
    [Column("device_id")]
    public Guid? DeviceId { get; set; }

    /// <summary>
    /// FK to <c>inventory_items.id</c>. Always null in Phase 1; populated by
    /// Phase 4's materialization once <c>inventory_items</c> exists. The
    /// FK constraint will be added in a later migration.
    /// </summary>
    [Column("inventory_item_id")]
    public Guid? InventoryItemId { get; set; }

    /// <summary>FK to <c>inventory_batches.id</c>. Null until Phase 4+.</summary>
    [Column("inventory_batch_id")]
    public Guid? InventoryBatchId { get; set; }

    [Column("serial_number")]
    [MaxLength(256)]
    public string? SerialNumber { get; set; }

    [Column("insertion_site")]
    [MaxLength(256)]
    public string? InsertionSite { get; set; }

    /// <summary>UTC. The moment this physical unit started being worn.</summary>
    [Column("started_at")]
    public DateTime StartedAt { get; set; }

    /// <summary>UTC. Null while the instance is still open.</summary>
    [Column("ended_at")]
    public DateTime? EndedAt { get; set; }

    /// <summary>Persisted as a string for forward-compatibility with new enum values.</summary>
    [Column("end_reason")]
    [MaxLength(32)]
    public string? EndReason { get; set; }

    [Column("notes")]
    [MaxLength(4096)]
    public string? Notes { get; set; }

    [Column("snapshot_wear_days")]
    public int? SnapshotWearDays { get; set; }

    [Column("snapshot_reservoir_capacity")]
    public decimal? SnapshotReservoirCapacity { get; set; }

    [Column("filled_units")]
    public decimal? FilledUnits { get; set; }

    [Column("residual_units")]
    public decimal? ResidualUnits { get; set; }

    [Column("sys_created_at")]
    public DateTime SysCreatedAt { get; set; } = DateTime.UtcNow;

    [Column("sys_updated_at")]
    public DateTime SysUpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Soft-delete tombstone. Records with a non-null value are invisible above the repo.</summary>
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}
