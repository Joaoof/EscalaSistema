using EscalaSistema.API.Enum;

namespace EscalaSistema.API.Models;

public class ScaleAssignment
{
    public Guid Id { get; set; }
    public Scale Scale { get; set; }
    public Guid ScaleId { get; set; }
    public Musician Musician { get; set; }
    public Guid MusicianId { get; set; }
    public InstrumentEnum Role { get; set; }
    public ConfirmationStatusEnum ConfirmationStatus { get; set; }
    public DateTime ConfirmedAt { get; set; }
}
