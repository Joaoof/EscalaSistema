namespace EscalaSistema.API.DTOs;

public class MusicResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string YoutubeUrl { get; set; }

    public Guid CultId { get; set; }
}
