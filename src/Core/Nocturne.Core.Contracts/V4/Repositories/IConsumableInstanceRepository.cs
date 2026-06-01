using Nocturne.Core.Models.V4;

namespace Nocturne.Core.Contracts.V4.Repositories;

/// <summary>
/// Repository port for <see cref="ConsumableInstance"/> records. RLS-scoped:
/// every method runs against the current tenant context.
/// </summary>
/// <seealso cref="ConsumableInstance"/>
public interface IConsumableInstanceRepository
{
    /// <summary>Returns the single open instance of the given kind for the current tenant, or null.</summary>
    Task<ConsumableInstance?> GetOpenByKindAsync(ConsumableKind kind, CancellationToken ct = default);

    /// <summary>Returns every open instance for the current tenant, indexed by <see cref="ConsumableInstance.Kind"/>.</summary>
    Task<IReadOnlyList<ConsumableInstance>> GetAllOpenAsync(CancellationToken ct = default);

    /// <summary>
    /// Returns the most recent N closed instances of the given kind, newest first.
    /// </summary>
    Task<IReadOnlyList<ConsumableInstance>> GetRecentClosedAsync(
        ConsumableKind kind, int limit, CancellationToken ct = default);

    /// <summary>Returns the most recent N closed instances across all kinds, newest first.</summary>
    Task<IReadOnlyList<ConsumableInstance>> GetRecentClosedAsync(int limit, CancellationToken ct = default);

    /// <summary>Returns a single instance by id, or null.</summary>
    Task<ConsumableInstance?> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Returns the instance opened by the given DeviceEvent (matched on
    /// <see cref="ConsumableInstance.SourceDeviceEventId"/>), or null when
    /// no instance was opened by that event. Used by the open-on-event hook
    /// for idempotency.
    /// </summary>
    Task<ConsumableInstance?> GetBySourceDeviceEventIdAsync(Guid deviceEventId, CancellationToken ct = default);

    /// <summary>Inserts a new instance row, returning it with its assigned id.</summary>
    Task<ConsumableInstance> CreateAsync(ConsumableInstance instance, CancellationToken ct = default);

    /// <summary>
    /// Updates a mutable subset of fields (notes, insertion site, serial, end reason,
    /// residual units, ended-at). Returns the updated instance, or null when not found.
    /// </summary>
    Task<ConsumableInstance?> UpdateAsync(ConsumableInstance instance, CancellationToken ct = default);

    /// <summary>Soft-deletes an instance. Returns true when the row existed and was tombstoned.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Returns true when there is any instance row for the current tenant.
    /// Used as the idempotency guard by the backfill hosted service.
    /// </summary>
    Task<bool> AnyExistAsync(CancellationToken ct = default);
}
