using EscalaSistema.API.Enum;

namespace EscalaSistema.API.DTOs;

public class AssignMusicianToScaleRequest
{
    public Guid MusicianId { get; set; }
    public InstrumentEnum Role { get; set; }
}

