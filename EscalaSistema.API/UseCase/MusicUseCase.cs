using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class MusicUseCase: IMusicUseCase
{
    private readonly IMusicRepository _musicRepository;
    private readonly IValidator<Music> _validator;

    public MusicUseCase(IMusicRepository musicRepository, IValidator<Music> validator)
    {
        _musicRepository = musicRepository;
        _validator = validator;
    }
    public async Task<MusicResponse> Register(CreateMusicRequest music)
    {
        var musicModel = new Music
        {
            Name = music.Name,
            Key = music.Key,
            YoutubeUrl = music.YoutubeUrl,
            CultId = music.CultId
        };

        var validationResult = await _validator.ValidateAsync(musicModel);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        await _musicRepository.AddAsync(musicModel);

        var response = new MusicResponse
        {
            Name = musicModel.Name,
            Key = musicModel.Key,
            YoutubeUrl = musicModel.YoutubeUrl,
            CultId = musicModel.CultId
        };
        return response;
    }

}
