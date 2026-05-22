using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nocturne.Core.Models;

namespace Nocturne.Infrastructure.Data.Entities;

/// <summary>
/// Immutable inventory stock movement ledger row.
/// </summary>
[Table("inventory_transactions")]
public class InventoryTransactionEntity : ITenantScoped
{
    [Column("tenant_id")]
    public Guid TenantId { get; set; }

    [Key]
    public Guid Id { get; set; }

    [Column("inventory_item_id")]
    public Guid InventoryItemId { get; set; }

    [Column("inventory_batch_id")]
    public Guid? InventoryBatchId { get; set; }

    [Column("type")]
    public InventoryTransactionType Type { get; set; }

    [Column("quantity_delta")]
    public decimal QuantityDelta { get; set; }

    [Column("quantity_after")]
    public decimal QuantityAfter { get; set; }

    [Column("reason")]
    [MaxLength(255)]
    public string? Reason { get; set; }

    [Column("source_type")]
    [MaxLength(64)]
    public string? SourceType { get; set; }

    [Column("source_id")]
    [MaxLength(128)]
    public string? SourceId { get; set; }

    [Column("source_timestamp")]
    public DateTime? SourceTimestamp { get; set; }

    [Column("notes")]
    [MaxLength(1000)]
    public string? Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(InventoryItemId))]
    public virtual InventoryItemEntity Item { get; set; } = null!;

    [ForeignKey(nameof(InventoryBatchId))]
    public virtual InventoryBatchEntity? Batch { get; set; }
}
