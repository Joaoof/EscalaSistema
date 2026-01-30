using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

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

    public Task<Musician?> GetByIdAsync(Guid id)
    {
        return _context.Musicians.FindAsync(id).AsTask();
    }

    public async Task<IEnumerable<Musician>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.Musicians
            .Where(m => ids.Contains(m.Id))
            .ToListAsync();
    }
}
