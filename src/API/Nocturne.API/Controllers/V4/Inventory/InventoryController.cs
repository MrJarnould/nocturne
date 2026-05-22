using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Remote.Attributes;
using Nocturne.Core.Contracts.Inventory;
using Nocturne.Core.Models;
using Nocturne.Core.Models.Inventory;

namespace Nocturne.API.Controllers.V4.Inventory;

/// <summary>
/// Diabetes supply inventory API. Inventory is tenant-wide; all members of a
/// tenant share the same supply list and see the same low-stock /
/// expiring-soon notifications (routed to the tenant owner).
/// </summary>
[ApiController]
[Tags("Inventory")]
[Route("api/v4/inventory")]
[Authorize]
[Produces("application/json")]
public class InventoryController(IInventoryService inventory) : ControllerBase
{
    [HttpGet("items")]
    [RemoteQuery]
    [ProducesResponseType(typeof(InventoryItemDto[]), StatusCodes.Status200OK)]
    public async Task<ActionResult<InventoryItemDto[]>> GetItems(
        [FromQuery] bool includeArchived = false,
        CancellationToken ct = default)
    {
        var items = await inventory.GetItemsAsync(includeArchived, ct);
        return Ok(items.ToArray());
    }

    [HttpGet("items/{id:guid}")]
    [RemoteQuery]
    [ProducesResponseType(typeof(InventoryItemDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventoryItemDetailDto>> GetItem(Guid id, CancellationToken ct = default)
    {
        var item = await inventory.GetItemAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>
    /// Returns the device catalog (CGMs / pumps / insulins) filtered to the
    /// given therapy mode. The frontend uses this to populate the multi-step
    /// seed wizard.
    /// </summary>
    [HttpGet("catalog")]
    [RemoteQuery]
    [ProducesResponseType(typeof(InventoryCatalogEntry[]), StatusCodes.Status200OK)]
    public ActionResult<InventoryCatalogEntry[]> GetInventoryCatalog(
        [FromQuery] TherapyMode mode = TherapyMode.Pump)
        => Ok(inventory.GetInventoryCatalog(mode).ToArray());

    /// <summary>
    /// Materializes inventory items based on the user's catalog selection plus
    /// the generic supply defaults (test strips, lancets, glucagon, etc.).
    /// Idempotent per (kind, name) — re-running is a no-op for already-seeded items.
    /// </summary>
    [HttpPost("seed")]
    [RemoteCommand(Invalidates = ["GetItems", "GetInventoryCatalog"])]
    [ProducesResponseType(typeof(InventoryItemDto[]), StatusCodes.Status200OK)]
    public async Task<ActionResult<InventoryItemDto[]>> SeedFromSelection(
        [FromBody] InventorySeedRequest request,
        CancellationToken ct = default)
    {
        var items = await inventory.SeedFromSelectionAsync(request, ct);
        return Ok(items.ToArray());
    }

    [HttpPost("items")]
    [RemoteCommand(Invalidates = ["GetItems"])]
    [ProducesResponseType(typeof(InventoryItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryItemDto>> CreateItem(
        [FromBody] InventoryItemRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var item = await inventory.CreateItemAsync(request, ct);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("items/{id:guid}")]
    [RemoteCommand(Invalidates = ["GetItems", "GetItem"])]
    [ProducesResponseType(typeof(InventoryItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryItemDto>> UpdateItem(
        Guid id,
        [FromBody] InventoryItemRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var item = await inventory.UpdateItemAsync(id, request, ct);
            return item is null ? NotFound() : Ok(item);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("items/{id:guid}")]
    [RemoteCommand(Invalidates = ["GetItems"])]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ArchiveItem(Guid id, CancellationToken ct = default)
    {
        return await inventory.ArchiveItemAsync(id, ct) ? NoContent() : NotFound();
    }

    [HttpGet("items/{itemId:guid}/batches")]
    [RemoteQuery]
    [ProducesResponseType(typeof(InventoryBatchDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventoryBatchDto[]>> GetBatches(Guid itemId, CancellationToken ct = default)
    {
        var item = await inventory.GetItemAsync(itemId, ct);
        return item is null ? NotFound() : Ok(item.Batches.ToArray());
    }

    [HttpGet("items/{itemId:guid}/transactions")]
    [RemoteQuery]
    [ProducesResponseType(typeof(InventoryTransactionDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventoryTransactionDto[]>> GetTransactions(Guid itemId, CancellationToken ct = default)
    {
        var item = await inventory.GetItemAsync(itemId, ct);
        return item is null ? NotFound() : Ok(item.Transactions.ToArray());
    }

    [HttpPost("items/{itemId:guid}/batches")]
    [RemoteCommand(Invalidates = ["GetItems", "GetItem", "GetBatches", "GetTransactions"])]
    [ProducesResponseType(typeof(InventoryBatchDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryBatchDto>> AddBatch(
        Guid itemId,
        [FromBody] InventoryBatchRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var batch = await inventory.AddBatchAsync(itemId, request, ct);
            return batch is null ? NotFound() : Created($"/api/v4/inventory/items/{itemId}/batches/{batch.Id}", batch);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("batches/{batchId:guid}")]
    [RemoteCommand(Invalidates = ["GetItems", "GetItem", "GetBatches"])]
    [ProducesResponseType(typeof(InventoryBatchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventoryBatchDto>> UpdateBatchMetadata(
        Guid batchId,
        [FromBody] InventoryBatchMetadataRequest request,
        CancellationToken ct = default)
    {
        var batch = await inventory.UpdateBatchMetadataAsync(batchId, request, ct);
        return batch is null ? NotFound() : Ok(batch);
    }

    [HttpPost("items/{itemId:guid}/consume")]
    [RemoteCommand(Invalidates = ["GetItems", "GetItem", "GetBatches", "GetTransactions"])]
    [ProducesResponseType(typeof(InventoryItemDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryItemDetailDto>> Consume(
        Guid itemId,
        [FromBody] InventoryConsumeRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var item = await inventory.ConsumeAsync(itemId, request, ct);
            return item is null ? NotFound() : Ok(item);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("batches/{batchId:guid}/adjust")]
    [RemoteCommand(Invalidates = ["GetItems", "GetItem", "GetBatches", "GetTransactions"])]
    [ProducesResponseType(typeof(InventoryItemDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryItemDetailDto>> AdjustBatch(
        Guid batchId,
        [FromBody] InventoryAdjustBatchRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var item = await inventory.AdjustBatchAsync(batchId, request, ct);
            return item is null ? NotFound() : Ok(item);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("batches/{batchId:guid}/expire")]
    [RemoteCommand(Invalidates = ["GetItems", "GetItem", "GetBatches", "GetTransactions"])]
    [ProducesResponseType(typeof(InventoryBatchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventoryBatchDto>> TransferBatchToExpired(
        Guid batchId,
        [FromBody] TransferBatchToExpiredRequest? request = null,
        CancellationToken ct = default)
    {
        var batch = await inventory.TransferBatchToExpiredAsync(batchId, request?.Notes, ct);
        return batch is null ? NotFound() : Ok(batch);
    }
}

public class TransferBatchToExpiredRequest
{
    public string? Notes { get; set; }
}
