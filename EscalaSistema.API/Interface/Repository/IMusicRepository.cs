using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface IMusicRepository
{
    Task<Music> GetByIdAsync(Guid id);
    Task<IEnumerable<Music>> GetAllAsync();
    Task AddAsync(Music music);
    Task DeleteAsync(Guid id);
}
