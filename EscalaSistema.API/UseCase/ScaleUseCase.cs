using EscalaSistema.API.Data;
using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Middleware;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.UseCase;

public class ScaleUseCase : ICreateScaleUseCase
{
    private readonly ICreateScaleRepository _repository;
    private readonly EscalaSistemaDbContext _context;
    public ScaleUseCase(ICreateScaleRepository repository, EscalaSistemaDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<Scale> Register(Guid cultId)
    {
        var exits = await _context.Scales.AnyAsync(s => s.CultId == cultId);

        if(exits)
            throw new DomainException(ScaleErrors.AlreadyExists);
        return await _repository.CreateAsync(cultId);
    }
}
