using EscalaSistema.API.DTOs;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.UseCase;

public interface IPublishScaleUseCase
{
    Task<Scale> Register (CreateScaleRequest scale);
}
