using Nocturne.Connectors.Core.Interfaces;
using Nocturne.Core.Contracts.Inventory;
using Nocturne.Core.Contracts.V4;
using Nocturne.Core.Contracts.V4.Repositories;
using Nocturne.Core.Models;
using Nocturne.Core.Models.V4;

namespace Nocturne.API.Services.ConnectorPublishing;

/// <summary>
/// Publishes device status and device event data received from connectors into
/// the Nocturne domain via <see cref="IDeviceStatusDecomposer"/> and <see cref="IDeviceEventRepository"/>.
/// </summary>
/// <seealso cref="IDevicePublisher"/>
internal sealed class DevicePublisher : IDevicePublisher
{
    private readonly IDeviceStatusDecomposer _decomposer;
    private readonly IDeviceEventRepository _deviceEventRepository;
    private readonly IInventoryService _inventory;
    private readonly ILogger<DevicePublisher> _logger;

    public DevicePublisher(
        IDeviceStatusDecomposer decomposer,
        IDeviceEventRepository deviceEventRepository,
        IInventoryService inventory,
        ILogger<DevicePublisher> logger)
    {
        _decomposer = decomposer ?? throw new ArgumentNullException(nameof(decomposer));
        _deviceEventRepository = deviceEventRepository ?? throw new ArgumentNullException(nameof(deviceEventRepository));
        _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> PublishDeviceStatusAsync(
        IEnumerable<DeviceStatus> deviceStatuses,
        string source,
        CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var ds in deviceStatuses)
            {
                await _decomposer.DecomposeAsync(ds, cancellationToken);
            }
            return true;
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish device status for {Source}", source);
            return false;
        }
    }

    public async Task<bool> PublishDeviceEventsAsync(
        IEnumerable<DeviceEvent> records,
        string source,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var recordList = records.ToList();
            if (recordList.Count == 0) return true;

            await _deviceEventRepository.BulkCreateAsync(recordList, cancellationToken);
            _logger.LogDebug("Published {Count} DeviceEvent records for {Source}", recordList.Count, source);

            // Fire the inventory auto-consume hook for each event.
            // BulkCreateAsync doesn't pass IDs back, but the records carry their
            // own IDs assigned by the repository (UUIDv7 generated client-side
            // before the bulk insert). The inventory ledger's
            // (source_type, source_id) unique index makes this idempotent if a
            // connector resync ever calls us twice for the same event.
            foreach (var deviceEvent in recordList)
            {
                try
                {
                    await _inventory.AutoConsumeForDeviceEventAsync(deviceEvent, cancellationToken);
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex,
                        "Inventory auto-consume failed for DeviceEvent {Id} ({EventType}) from {Source}; "
                        + "supply stock not updated",
                        deviceEvent.Id, deviceEvent.EventType, source);
                }
            }

            return true;
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish DeviceEvent records for {Source}", source);
            return false;
        }
    }
}
