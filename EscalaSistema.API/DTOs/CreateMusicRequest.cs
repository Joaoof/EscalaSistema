namespace EscalaSistema.API.DTOs;

public class CreateMusicRequest
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string YoutubeUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
}
