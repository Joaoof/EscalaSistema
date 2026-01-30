using EscalaSistema.API.Data;
using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Repository;

public class ScaleRepository : ICreateScaleRepository
{
    private readonly EscalaSistemaDbContext _context;

    public ScaleRepository(EscalaSistemaDbContext context)
    {
        _context = context;
        
    }
    public async Task<Scale> CreateAsync(Guid cultId)
    {
        var cultExists = await _context.Cults.AnyAsync(c => c.Id == cultId);
        if (!cultExists)
            throw new DomainException(CultErrors.CultNotFound);

        var scale = new Scale
        {
            Id = Guid.NewGuid(),
            CultId = cultId,
            IsPublished = false,
            IsClosed = false,
        };

        _context.Scales.Add(scale);
        await _context.SaveChangesAsync();

        return scale;
    }
    public async Task<List<Scale>> GetByMonthAsync(int month, int year)
    {
        return await _context.Scales
            .Include(s => s.Cult) // Precisamos da data do culto
            .Include(s => s.ScaleAssignments) // Precisamos saber quem já está escalado
            .Where(s => s.Cult.DateTime.Month == month && s.Cult.DateTime.Year == year)
            .ToListAsync();
    }

    public async Task CloseScale()
    {
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Scale?> GetByIdAsync(Guid id)
    {
        return await _context.Scales
            .Include(s => s.ScaleAssignments) // Importante carregar a lista!
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public void Add(Scale scale)
    {
        _context.Scales.Add(scale);
    }

    public async Task<List<Scale>> GetByCultIdsAsync(List<Guid> cultIds)
    {
        return await _context.Scales
            .Include(s => s.Cult)
            .Include(s => s.ScaleAssignments) // Importante trazer os músicos!
            .Where(s => cultIds.Contains(s.CultId))
            .ToListAsync();
    }
}
