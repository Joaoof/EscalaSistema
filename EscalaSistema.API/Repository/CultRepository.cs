using EscalaSistema.API.Data;
using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Repository;

public class CultRepository : ICultRepository
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

    public async Task<CultResponse> AddAsync(CultRequest cult)
    {
        var register = await _context.Cults.AddAsync(new Cult(
            cult.Name,
            cult.DateTime
        ));
        await _context.SaveChangesAsync();

        return new CultResponse
        {
            Name = register.Entity.Name,
            Date = register.Entity.DateTime,
        };
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

    public async Task<CultResponse> GetByNameCult(string name)
    {
        var search = await _context.Cults.FirstOrDefaultAsync(n => n.Name == name);

        return new CultResponse
        {
            Name = search.Name,
            Date = search.DateTime,
        };
    }

    public async Task<CultResponse> GetByIdCult(Guid id)
    {
        var search = await _context.Cults.FindAsync(id);
        return new CultResponse
        {
            Name = search.Name,
            Date = search.DateTime,
        };
    }

    public async Task<CultResponse> GetByDateTime(DateTime date)
    {
        var data = await _context.Cults.FirstOrDefaultAsync(d => d.DateTime == date);

        return new CultResponse
        {
            Name = data.Name,
            Date = data.DateTime
        };
    }
}
