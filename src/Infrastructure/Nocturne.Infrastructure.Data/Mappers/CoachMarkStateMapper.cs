namespace Nocturne.Infrastructure.Data.Mappers;

using Nocturne.Core.Models.CoachMarks;
using Nocturne.Infrastructure.Data.Entities;

public static class CoachMarkStateMapper
{
    public static CoachMarkState ToDomainModel(CoachMarkStateEntity entity)
    {
        return new CoachMarkState
        {
            Id = entity.Id,
            SubjectId = entity.SubjectId,
            MarkKey = entity.MarkKey,
            Status = entity.Status,
            SeenAt = entity.SeenAt,
            CompletedAt = entity.CompletedAt,
        };
    }

    public static CoachMarkStateEntity ToEntity(CoachMarkState model, Guid tenantId)
    {
        return new CoachMarkStateEntity
        {
            Id = model.Id == Guid.Empty ? Guid.CreateVersion7() : model.Id,
            TenantId = tenantId,
            SubjectId = model.SubjectId,
            MarkKey = model.MarkKey,
            Status = model.Status,
            SeenAt = model.SeenAt,
            CompletedAt = model.CompletedAt,
        };
    }
}
