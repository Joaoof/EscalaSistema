using EscalaSistema.API.DTOs;

namespace EscalaSistema.API.Interface.UseCase;

public interface IMusicUseCase
{
    Task<MusicResponse> Register(CreateMusicRequest createMusicRequest);
}
