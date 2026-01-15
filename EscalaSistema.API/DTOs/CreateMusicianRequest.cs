using EscalaSistema.API.Enum;

namespace EscalaSistema.API.DTOs;

public class CreateMusicianRequest
{
    public string Name { get; set; }
    public InstrumentEnum Instrument { get; set; }
}
