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

    public async Task<Scale> UpdateAsync(Guid Id, Scale scale) 
    {
        var existingScale = await _context.Scales.FindAsync(Id);
        if (existingScale == null)
            throw new DomainException(CultErrors.CultNotFound);

        existingScale.CultId = scale.CultId;
        existingScale.IsPublished = scale.IsPublished;
        existingScale.IsClosed = scale.IsClosed;

        _context.Scales.Update(existingScale);
        await _context.SaveChangesAsync();
        return existingScale;
    }

    public async Task<List<Scale>> GetByAllScale()
    {
        var scales = await _context.Scales.ToListAsync();

        return scales;  
    }
}
