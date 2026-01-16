namespace EscalaSistema.API.DTOs;

public class CreateMusicianResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Instrument { get; set; }
    public bool IsActive { get; set; }
}
