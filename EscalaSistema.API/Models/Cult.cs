namespace EscalaSistema.API.Models;

public class Cult
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid MusicId { get; set; }
    public ICollection<Music> Musics { get; set; }
    public Scale Scale { get; set; }

}
