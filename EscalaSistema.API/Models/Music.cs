namespace EscalaSistema.API.Models;

public class Music
{
    public Guid Id {  get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string YoutubeUrl { get; set; }
    public Cult Cult { get; set; }
    public Guid CultId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    }
