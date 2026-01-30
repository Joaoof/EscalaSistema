using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;
public interface IMusicianRepository
{
    Task<IEnumerable<Musician>> GetByIdsAsync(List<Guid> ids);
    Task<Musician> CreateAsync(Musician musician);
    Task<Musician?> GetByIdAsync(Guid id);
}
