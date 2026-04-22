namespace Nocturne.Core.Contracts.CoachMarks;

using Nocturne.Core.Models.CoachMarks;

public interface ICoachMarkService
{
    Task<IReadOnlyList<CoachMarkState>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CoachMarkState> UpsertAsync(string markKey, string status, CancellationToken cancellationToken = default);
}
