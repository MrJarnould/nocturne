using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Remote.Attributes;
using Nocturne.Core.Contracts.V4;
using Nocturne.Core.Models.V4;

namespace Nocturne.API.Controllers.V4.Health;

/// <summary>
/// Read + edit surface for <see cref="ConsumableInstance"/> wear sessions.
/// The write surface for opening / closing is driven by the
/// <c>DeviceEventController</c> + <c>TreatmentDecomposer</c> hook, not by
/// this controller. Manual edits are limited to the editable subset
/// captured by <see cref="ConsumableInstanceEditRequest"/>.
/// </summary>
[ApiController]
[Tags("Health")]
[Route("api/v4/consumable-instances")]
[Authorize]
[Produces("application/json")]
public class ConsumableInstanceController : ControllerBase
{
    private readonly IConsumableInstanceService _service;

    public ConsumableInstanceController(IConsumableInstanceService service)
    {
        _service = service;
    }

    /// <summary>List the currently-open consumable wear sessions for this tenant.</summary>
    [HttpGet("active")]
    [RemoteQuery]
    [ProducesResponseType(typeof(IReadOnlyList<ConsumableInstance>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ConsumableInstance>>> GetActive(CancellationToken ct = default)
    {
        var active = await _service.GetActiveAsync(ct);
        return Ok(active);
    }

    /// <summary>
    /// List the most recently-closed wear sessions across all consumable kinds,
    /// newest end-time first.
    /// </summary>
    /// <param name="limit">Maximum number of records to return (default 25, max 200).</param>
    [HttpGet("recent")]
    [RemoteQuery]
    [ProducesResponseType(typeof(IReadOnlyList<ConsumableInstance>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ConsumableInstance>>> GetRecentClosed(
        [FromQuery] int limit = 25,
        CancellationToken ct = default)
    {
        var clamped = Math.Clamp(limit, 1, 200);
        var recent = await _service.GetRecentClosedAsync(clamped, ct);
        return Ok(recent);
    }

    /// <summary>Get one instance by id.</summary>
    [HttpGet("{id:guid}")]
    [RemoteQuery]
    [ProducesResponseType(typeof(ConsumableInstance), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConsumableInstance>> GetById(Guid id, CancellationToken ct = default)
    {
        var instance = await _service.GetByIdAsync(id, ct);
        return instance is null ? NotFound() : Ok(instance);
    }

    /// <summary>
    /// Patch the editable fields of a consumable instance (insertion site,
    /// serial number, notes, ended-at, end reason, residual units).
    /// </summary>
    [HttpPatch("{id:guid}")]
    [RemoteForm(Invalidates = ["GetActive", "GetRecentClosed", "GetById"])]
    [ProducesResponseType(typeof(ConsumableInstance), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConsumableInstance>> Update(
        Guid id,
        [FromBody] ConsumableInstanceEditRequest request,
        CancellationToken ct = default)
    {
        var updated = await _service.UpdateAsync(id, request, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>Soft-delete a consumable instance.</summary>
    [HttpDelete("{id:guid}")]
    [RemoteCommand(Invalidates = ["GetActive", "GetRecentClosed", "GetById"])]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        var deleted = await _service.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}
