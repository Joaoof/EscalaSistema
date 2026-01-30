using EscalaSistema.API.Enum;

namespace EscalaSistema.API.DTOs;

public class BulkAssignRequest
{
    public int Month { get; set; }
    public int Year { get; set; }
    public List<MusicianBulkAssignment> Assignments { get; set; } = new();
}

public class MusicianBulkAssignment
{
    public Guid MusicianId { get; set; }
    public InstrumentEnum Role { get; set; } // O papel (Bateria, Voz, etc.)
    public List<Guid> CultIds { get; set; } = new(); // Lista de Cultos onde ele vai tocar
}
