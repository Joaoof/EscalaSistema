namespace EscalaSistema.API.DTOs;

public class ScalePublishResponse
{
    public Guid ScaleId { get; set; }
    public bool IsPublished { get; set; }
    public DateTime PublishedAt { get; set; }
}
