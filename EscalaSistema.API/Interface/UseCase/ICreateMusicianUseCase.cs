using EscalaSistema.API.DTOs;

namespace EscalaSistema.API.Interface.UseCase;

public interface ICreateMusicianUseCase
{
    Task<CreateMusicianResponse> Execute(CreateMusicianRequest request, Guid userId);
}

