using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nocturne.Core.Models;

namespace Nocturne.Infrastructure.Data.Entities;

/// <summary>
/// Tenant-wide diabetes supply inventory definition. One row per SKU the
/// tenant tracks. Auto-consume topology is per-row (Bolus/BasalInjection for
/// MDI insulin items, DeviceEvent for hardware items). For pump users, the
/// insulin "bottle" item drains via the LinkedInsulin link from a Pod /
/// Reservoir item's DeviceEvent rather than via Bolus.
/// </summary>
[Table("inventory_items")]
public class InventoryItemEntity : ITenantScoped
{
    [Column("tenant_id")]
    public Guid TenantId { get; set; }

    [Key]
    public Guid Id { get; set; }

    [Column("name")]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Column("category")]
    public InventoryCategory Category { get; set; }

    [Column("kind")]
    public InventoryKind Kind { get; set; }

    [Column("unit_label")]
    [MaxLength(64)]
    public string UnitLabel { get; set; } = "each";

    [Column("low_stock_threshold")]
    public decimal LowStockThreshold { get; set; }

    [Column("target_stock")]
    public decimal? TargetStock { get; set; }

    [Column("auto_consume_enabled")]
    public bool AutoConsumeEnabled { get; set; }

    [Column("auto_consume_source")]
    public InventoryAutoConsumeSource AutoConsumeSource { get; set; } = InventoryAutoConsumeSource.None;

    [Column("patient_insulin_id")]
    public Guid? PatientInsulinId { get; set; }

    [Column("device_event_types_json", TypeName = "jsonb")]
    public string DeviceEventTypesJson { get; set; } = "[]";

    /// <summary>
    /// For Pod / Reservoir items: the InventoryItem (Kind = Insulin) that
    /// this physical container draws from when changed. When this item's
    /// auto-consume fires, the service also consumes
    /// <see cref="LinkedInsulinUnitsPerUse"/> from the linked insulin item.
    /// </summary>
    [Column("linked_insulin_item_id")]
    public Guid? LinkedInsulinItemId { get; set; }

    /// <summary>
    /// Units of insulin to deduct from the linked insulin item each time this
    /// item's auto-consume fires (e.g. 200 for an Omnipod, 300 for a Tandem
    /// reservoir).
    /// </summary>
    [Column("linked_insulin_units_per_use")]
    public decimal? LinkedInsulinUnitsPerUse { get; set; }

    /// <summary>
    /// Expected wear duration in days for items where each unit lasts a known
    /// fixed period (CGM sensors: 10/14/15 days, pods: 3 days, infusion sets:
    /// 3 days, etc.). Drives deterministic run-out projection. Null for items
    /// consumed at a variable rate (insulin, strips, lancets) — those project
    /// from historical consumption rate instead.
    /// </summary>
    [Column("wear_days")]
    public int? WearDays { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("is_archived")]
    public bool IsArchived { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<InventoryBatchEntity> Batches { get; set; } = new List<InventoryBatchEntity>();
    public virtual ICollection<InventoryTransactionEntity> Transactions { get; set; } = new List<InventoryTransactionEntity>();
}
