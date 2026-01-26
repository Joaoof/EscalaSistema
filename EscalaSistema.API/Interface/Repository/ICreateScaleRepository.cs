using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface ICreateScaleRepository
{
    Task<Scale> CreateAsync(Guid cultId);
    Task<List<Scale>> GetByAllScale();
    Task<Scale> UpdateAsync(Guid Id, Scale scale);

}
