using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nocturne.Infrastructure.Data.Entities;

/// <summary>
/// Audit log entry recording a mutation to a clinical entity.
/// Append-only — no updates or deletes.
/// </summary>
[Table("mutation_audit_log")]
public class MutationAuditLogEntity : ITenantScoped
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("tenant_id")]
    public Guid TenantId { get; set; }

    [Required]
    [Column("entity_type")]
    [MaxLength(100)]
    public string EntityType { get; set; } = null!;

    [Column("entity_id")]
    public Guid EntityId { get; set; }

    [Required]
    [Column("action")]
    [MaxLength(10)]
    public string Action { get; set; } = null!;

    [Column("changes", TypeName = "jsonb")]
    public string? ChangesJson { get; set; }

    [Column("subject_id")]
    public Guid? SubjectId { get; set; }

    [Column("auth_type")]
    [MaxLength(50)]
    public string? AuthType { get; set; }

    [Column("ip_address")]
    [MaxLength(45)]
    public string? IpAddress { get; set; }

    [Column("token_id")]
    public Guid? TokenId { get; set; }

    [Column("correlation_id")]
    [MaxLength(50)]
    public string? CorrelationId { get; set; }

    [Column("endpoint")]
    [MaxLength(200)]
    public string? Endpoint { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
