namespace Nocturne.Core.Models.CoachMarks;

public class CoachMarkState
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }
    public string MarkKey { get; set; } = string.Empty;
    public string Status { get; set; } = "unseen"; // unseen, seen, dismissed, completed
    public DateTime? SeenAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
