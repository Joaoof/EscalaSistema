using EscalaSistema.API.DTOs;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.UseCase;

public interface ICultUseCase
{
    Task<Cult> Register(CultResponse cultResponse);
    Task<IEnumerable<Cult>> GetAllCultsAsync();
}
