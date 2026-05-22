using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nocturne.Core.Models;

namespace Nocturne.Infrastructure.Data.Entities;

/// <summary>
/// Stock batch for an inventory item.
/// </summary>
[Table("inventory_batches")]
public class InventoryBatchEntity : ITenantScoped
{
    [Column("tenant_id")]
    public Guid TenantId { get; set; }

    [Key]
    public Guid Id { get; set; }

    [Column("inventory_item_id")]
    public Guid InventoryItemId { get; set; }

    [Column("received_quantity")]
    public decimal ReceivedQuantity { get; set; }

    [Column("remaining_quantity")]
    public decimal RemainingQuantity { get; set; }

    [Column("received_at")]
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    [Column("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [Column("lot_number")]
    [MaxLength(128)]
    public string? LotNumber { get; set; }

    [Column("storage_state")]
    public InventoryStorageState StorageState { get; set; } = InventoryStorageState.Normal;

    [Column("notes")]
    [MaxLength(1000)]
    public string? Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey(nameof(InventoryItemId))]
    public virtual InventoryItemEntity Item { get; set; } = null!;
}
