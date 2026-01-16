using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.UseCase;

public class CreateMusicianUseCase: ICreateMusicianUseCase
{
    private readonly IMusicianRepository _musicianRepository;

    public CreateMusicianUseCase(IMusicianRepository musicianRepository)
    {
        _musicianRepository = musicianRepository;
    }

    public async Task<CreateMusicianResponse> Execute(CreateMusicianRequest request, Guid userId)
    {
        var musician = new Musician
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Instrument = request.Instrument.ToString().Trim(),
            IsActive = true,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _musicianRepository.CreateAsync(musician);

        return new CreateMusicianResponse
        {
            Id = created.Id,
            Name = created.Name,
            Instrument = created.Instrument,
            IsActive = created.IsActive
        };
    }
}
