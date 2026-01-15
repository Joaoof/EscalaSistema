using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface ICreateScaleRepository
{
    Task<Scale> CreateAsync(Guid cultId);

}
