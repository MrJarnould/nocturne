namespace Nocturne.Core.Models.V4;

/// <summary>
/// A single wear session of a consumable — the period during which a specific
/// pod / infusion set / CGM sensor / etc. was actually worn on the patient's
/// body. Sits between batches (what the patient bought) and the event stream
/// (what arrived in uploads), providing the canonical "this physical unit was
/// active from T1 to T2" record that lot-recall, day-N effect analyses, and
/// rotation planners all read.
/// </summary>
/// <remarks>
/// <para>
/// Instances are opened by <see cref="DeviceEventType.SiteChange"/> and
/// <see cref="DeviceEventType.SensorStart"/> events (manual UI actions can
/// also open / close one directly) and are closed when the next matching
/// event arrives or when the patient marks the previous one ended.
/// </para>
/// <para>
/// In Phase 1 of the inventory rollout, the link to inventory items is
/// intentionally left null and is populated by Phase 4's materialization
/// pass. The catalog-derived snapshot fields
/// (<see cref="SnapshotWearDays"/>, <see cref="SnapshotReservoirCapacity"/>)
/// are filled at open time from <see cref="ConsumableCatalog"/> so that
/// historical analytics remain stable across future catalog edits.
/// </para>
/// </remarks>
/// <seealso cref="ConsumableCatalogEntry"/>
/// <seealso cref="ConsumableKind"/>
/// <seealso cref="DeviceEvent"/>
/// <seealso cref="PatientDevice"/>
public class ConsumableInstance
{
    /// <summary>UUID v7 primary key.</summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Reference to the <see cref="DeviceEvent"/> that opened this instance.
    /// Used as the idempotency key by the open-on-event hook so that a
    /// resync of the same upstream event doesn't open duplicate instances.
    /// Null when the instance was opened by a manual UI action.
    /// </summary>
    public Guid? SourceDeviceEventId { get; set; }

    /// <summary>
    /// Foreign key to <see cref="ConsumableCatalogEntry.Id"/>. Identifies what
    /// kind of consumable this instance is — pod / infusion set / sensor /
    /// transmitter — and which device model it belongs to.
    /// </summary>
    public string ConsumableCatalogId { get; set; } = string.Empty;

    /// <summary>
    /// The role of this consumable, denormalized from the catalog entry for
    /// fast filtering ("find the open Pod instance for this tenant"). Stays
    /// in sync with <see cref="ConsumableCatalogId"/>.
    /// </summary>
    public ConsumableKind Kind { get; set; }

    /// <summary>
    /// Optional reference to the <see cref="V4.PatientDevice"/> the patient
    /// was using when this instance was opened. Lets analytics tie back to
    /// patient-curated device metadata (AID algorithm, manufacturer overrides,
    /// notes).
    /// </summary>
    public Guid? PatientDeviceId { get; set; }

    /// <summary>
    /// Optional reference to the auto-discovered <see cref="V4.Device"/> row
    /// that produced the event opening this instance.
    /// </summary>
    public Guid? DeviceId { get; set; }

    /// <summary>
    /// Inventory item the consumed unit came from. Null in Phase 1; populated
    /// by Phase 4's materialization once <c>inventory_items</c> exists.
    /// </summary>
    public Guid? InventoryItemId { get; set; }

    /// <summary>Inventory batch the consumed unit came from. Null until Phase 4 wiring.</summary>
    public Guid? InventoryBatchId { get; set; }

    /// <summary>Serial / lot number written on the unit's box, if known.</summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Free-text insertion site description (e.g. "left abdomen", "right
    /// thigh"). Phase 1 ships free-text; a structured rotation analytics
    /// catalog can land later without migrating data.
    /// </summary>
    public string? InsertionSite { get; set; }

    /// <summary>When the patient started wearing this unit (UTC).</summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// When the unit stopped being worn (UTC). Null while the instance is
    /// still open.
    /// </summary>
    public DateTime? EndedAt { get; set; }

    /// <summary>Why the unit stopped being worn. Null until ended.</summary>
    public ConsumableInstanceEndReason? EndReason { get; set; }

    /// <summary>Free-text notes about this specific instance.</summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Snapshot of <see cref="ConsumableCatalogEntry.WearDays"/> at open time.
    /// Persisted locally so historical day-N analytics remain stable when the
    /// catalog gains a new wear-day for a future product revision.
    /// </summary>
    public int? SnapshotWearDays { get; set; }

    /// <summary>
    /// Snapshot of pump-pod / reservoir capacity (units) at open time.
    /// Sourced from <see cref="PumpCapability.ReservoirCapacityUnits"/> on
    /// the parent device. Null for non-pump instances.
    /// </summary>
    public decimal? SnapshotReservoirCapacity { get; set; }

    /// <summary>
    /// Units of insulin actually filled at open time. May override the
    /// catalog default when the patient fills below capacity.
    /// </summary>
    public decimal? FilledUnits { get; set; }

    /// <summary>
    /// Residual units left when the instance was closed. Set together with
    /// <see cref="EndedAt"/>.
    /// </summary>
    public decimal? ResidualUnits { get; set; }

    /// <summary>When the record was first written (UTC).</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>When the record was last modified (UTC).</summary>
    public DateTime ModifiedAt { get; set; }
}
