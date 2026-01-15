namespace EscalaSistema.API.Models; 

public class Scale
{
    public Guid Id { get; set; }
    public Guid CultId { get; set; }
    public Cult Cult { get; set; }
    public bool IsPublished { get; set; }
    public bool IsClosed { get; set; }
    public DateTime PublishedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<ScaleAssignment> ScaleAssignments { get; set; }

    public void Publish()
    {
        IsPublished = true;
        IsClosed = true;
        PublishedAt = DateTime.Now;
    }
}
