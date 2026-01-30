using EscalaSistema.API.Domain.Errors;

namespace EscalaSistema.API.Models;

public class Cult
{
    private readonly List<Music> _musics = [];

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime DateTime { get; private set; }
    public bool IsPublished { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Guid MusicId { get; private set; }
    public IReadOnlyCollection<Music> Musics => _musics.AsReadOnly();
    public Scale Scale { get; private set; }

    public Cult(string name, DateTime dateTime)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(CultErrors.InvalidNameCult);

        if (name.Length < 3)
            throw new DomainException(CultErrors.InvalidNameCultTooShort);

        if (name.Length >= 500)
            throw new DomainException(CultErrors.InvalidNameCultTooLong);

        if (dateTime < DateTime.UtcNow)
            throw new DomainException(CultErrors.InvalidDateTimeCannotPast);

        Name = name;
        DateTime = dateTime;
        IsPublished = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Publicar()
    {
        if (IsPublished)
            throw new DomainException(CultErrors.IsPublished);

        if (DateTime < DateTime.UtcNow)
            throw new DomainException(CultErrors.CannotPublishPastCult);

        IsPublished = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(CultErrors.InvalidNameCult);

        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDateTime(DateTime dateTime)
    {
        if (dateTime < DateTime.UtcNow)
            throw new DomainException(CultErrors.InvalidDateTimeCannotPast);

        DateTime = dateTime;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateMusicId(Guid musicId)
    {
        MusicId = musicId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddMusic(Music music)
    {
        if (music is null)
            throw new DomainException(MusicErrors.MusicNotFound);

        if (_musics.Count >= 10)
            throw new DomainException(MusicErrors.MusicCount);

        _musics.Add(music);
        UpdatedAt = DateTime.UtcNow;
    }
}