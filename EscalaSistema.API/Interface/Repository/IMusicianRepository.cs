using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;
public interface IMusicianRepository
{
    Task<Musician> CreateAsync(Musician musician);
}
