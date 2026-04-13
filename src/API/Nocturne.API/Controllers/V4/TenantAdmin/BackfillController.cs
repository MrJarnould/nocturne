using Microsoft.AspNetCore.Mvc;
using Nocturne.API.Attributes;
using Nocturne.API.Authorization;
using Nocturne.API.Services.V4;

namespace Nocturne.API.Controllers.V4.TenantAdmin;

/// <summary>
/// Admin controller for triggering V4 backfill operations.
/// Decomposes all existing legacy entries and treatments into the v4 granular tables.
/// </summary>
[ApiController]
[Route("api/v4/admin")]
[RequireAdmin]
[Produces("application/json")]
[AllowDuringSetup]
public class BackfillController : ControllerBase
{
    private readonly V4BackfillService _backfillService;
    private readonly ILogger<BackfillController> _logger;
    private static readonly SemaphoreSlim BackfillLock = new(1, 1);

    public BackfillController(
        V4BackfillService backfillService,
        ILogger<BackfillController> logger)
    {
        _backfillService = backfillService;
        _logger = logger;
    }

    /// <summary>Triggers a backfill operation to recalculate derived data.</summary>
    [HttpPost("backfill")]
    [ProducesResponseType(typeof(BackfillResult), 200)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    /// <summary>Triggers a backfill operation to recalculate derived data.</summary>
    public async Task<ActionResult<BackfillResult>> TriggerBackfill(CancellationToken ct)
    {
        if (!await BackfillLock.WaitAsync(0, ct))
        {
            return Problem(detail: "A backfill operation is already in progress", statusCode: 409, title: "Conflict");
        }

        try
        {
            _logger.LogInformation("V4 backfill triggered via admin endpoint");
            var result = await _backfillService.BackfillAsync(ct);
            return Ok(result);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("V4 backfill was cancelled");
            return StatusCode(499, new { status = "cancelled" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "V4 backfill failed");
            return Problem(detail: ex.Message, statusCode: 500, title: "Internal Server Error");
        }
        finally
        {
            BackfillLock.Release();
        }
    }
}
