using Nocturne.Core.Models.V4;
using Nocturne.Infrastructure.Data.Entities.V4;

namespace Nocturne.Infrastructure.Data.Mappers.V4;

public static class DecompositionBatchMapper
{
    public static DecompositionBatch ToModel(DecompositionBatchEntity entity)
    {
        return new DecompositionBatch
        {
            Id = entity.Id,
            Source = entity.Source,
            SourceRecordId = entity.SourceRecordId,
            SourceTreatmentId = entity.SourceTreatmentId,
            CreatedAt = entity.CreatedAt,
        };
    }

    public static DecompositionBatchEntity ToEntity(DecompositionBatch model, Guid tenantId)
    {
        return new DecompositionBatchEntity
        {
            Id = model.Id,
            TenantId = tenantId,
            Source = model.Source,
            SourceRecordId = model.SourceRecordId,
            SourceTreatmentId = model.SourceTreatmentId,
            CreatedAt = model.CreatedAt,
        };
    }
}
