using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.UseCase;

public interface ICreateScaleUseCase
{
    Task<Scale> Register(Guid cultId);
}
