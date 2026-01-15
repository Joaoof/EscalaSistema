using EscalaSistema.API.DTOs;

namespace EscalaSistema.API.Interface.Repository;

public interface IAssignMusicianRepository
{
    Task AssignMusicianAsync(Guid scaleId, AssignMusicianToScaleRequest request);
}
