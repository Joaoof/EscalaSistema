using EscalaSistema.API.Data;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Repository;

public class CultRepository
{
    private readonly EscalaSistemaDbContext _context;

    public CultRepository(EscalaSistemaDbContext context)
    {
        _context = context;
    }

    public async Task<Cult> GetByIdASync(Guid id)
    {
        return await _context.Cults.FindAsync(id);
    }

    public async Task<Cult> GetByNameAsync(string name)
    {
        return await _context.Cults.FirstOrDefaultAsync(n => n.Name == name);
    } 

    public async Task<IEnumerable<Cult>> GetAllAsync()
    {
        return await _context.Cults.ToListAsync();
    }

    public async Task AddAsync(Cult cult)
    {
        await _context.Cults.AddAsync(cult);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var cult = await GetByIdASync(id);
        if (cult != null)
        {
            _context.Cults.Remove(cult);
            await _context.SaveChangesAsync();
        }
    }
}
