using EscalaSistema.API.DTOs;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.UseCase;

public interface ICreateScaleUseCase
{
    //Task<List<Scale>> Get();
    Task<Scale> Register(Guid cultId);
    Task<ScaleResponse> ScaleClosed(Guid id);
}
