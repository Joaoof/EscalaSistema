using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.UseCase;

public class ScaleUseCase : ICreateScaleUseCase
{
    private readonly ICreateScaleRepository _repository;

    public ScaleUseCase(ICreateScaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Scale> Register(Guid cultId)
    {
        return await _repository.CreateAsync(cultId);
    }
}
