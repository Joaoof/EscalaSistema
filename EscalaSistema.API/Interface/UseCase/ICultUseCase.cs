using EscalaSistema.API.DTOs;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.UseCase;

public interface ICultUseCase
{
    Task<CultResponse> Register(CultRequest cult);
    Task<IEnumerable<Cult>> GetAllCultsAsync();
}
