namespace EscalaSistema.API.DTOs;

public class ScaleResponse
{
    public Guid Id { get; set; }
    public Guid CultId { get; set; }

    public bool IsPublished { get; set; }
    public bool IsClosed { get; set; }

    public DateTime? PublishedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int AssignmentsCount { get; set; }
}
