namespace EscalaSistema.API.DTOs;

public class CultResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public bool IsPublished { get; set; }
    public bool IsClosed { get; set; }
}
