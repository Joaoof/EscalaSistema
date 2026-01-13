using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface ICultRepository
{
    Task<Cult> GetByIdASync(Guid id);
    Task<Cult> GetByNameAsync(string name);
    Task<IEnumerable<Cult>> GetAllAsync();
    Task AddAsync(Cult cult);
    Task DeleteAsync(Guid id);
}
