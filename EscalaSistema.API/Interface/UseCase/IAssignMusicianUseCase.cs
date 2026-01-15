using EscalaSistema.API.DTOs;

namespace EscalaSistema.API.Interface.UseCase;

public interface IAssignMusicianUseCase
{
    Task Register(Guid scaleId, AssignMusicianToScaleRequest assignMusicianToScaleRequest);
}
