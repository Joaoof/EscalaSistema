using EscalaSistema.API.Data;
using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
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

    public async Task<Scale> Update(Guid Id, Scale scale)
    {
        var cultExists = await _context.Cults.AnyAsync(c => c.Id == scale.CultId);
        if (!cultExists)
            throw new DomainException(CultErrors.CultNotFound);

        return await _repository.UpdateAsync(Id, scale);
    }

    public async Task<List<Scale>> Get()
    {
        var get = await _repository.GetByAllScale();

        return get;
    }
}
