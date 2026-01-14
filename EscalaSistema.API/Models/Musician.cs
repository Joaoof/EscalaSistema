namespace EscalaSistema.API.Models;

public class Musician
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Instrument { get; set; }
    public bool IsActive { get; set; }
    public Guid UserId { get; set; }
    public ICollection<ScaleAssignment> ScaleAssignments { get; set; }  
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
