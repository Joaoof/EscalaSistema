using EscalaSistema.API.Domain.Errors;

namespace EscalaSistema.API.Models;

public class Cult
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime DateTime { get; private set; }
    public bool IsPublished { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid MusicId { get; private set; }
    public ICollection<Music> Musics { get; private set; }
    public Scale Scale { get; private set; }

    public Cult(string name, DateTime dateTime)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(CultErrors.InvalidNameCult);

        if (dateTime < DateTime.Now)
            throw new DomainException(CultErrors.InvalidDateTimeCannotFuture);

        Name = name;
        DateTime = dateTime;
        IsPublished = false; 
    }

    public void Publicar()
    {
        IsPublished = true;
    }
}
