using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.Repository;

public class MusicianRepository : IMusicianRepository
{
    private readonly EscalaSistemaDbContext _context;

    public MusicianRepository(EscalaSistemaDbContext context)
    {
        _context = context;
    } 
    public async Task<Musician> CreateAsync(Musician musician)
    {
        await _context.Musicians.AddAsync(musician);
        await _context.SaveChangesAsync();
        return musician;
    }
}
