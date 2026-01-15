using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Middleware;
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
            throw new NotFoundException("Culto não encontrado");

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
}
