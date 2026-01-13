using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class CultUseCase : ICultUseCase
{
    private readonly ICultRepository _cultRepository;
    private readonly IValidator<Cult> _validator;
    public CultUseCase(ICultRepository cultRepository, IValidator<Cult> validator)
    {
        _cultRepository = cultRepository;
        _validator = validator;
    }

    public async Task<Cult> Register(CultResponse cultResponse)
    {
        var cult = new Cult
        {
            Id = Guid.NewGuid(),
            Name = cultResponse.Name,
            DateTime = cultResponse.Date,
        };

        var validationResult = await _validator.ValidateAsync(cult);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _cultRepository.AddAsync(cult);
        return cult;

    }

    public async Task<IEnumerable<Cult>> GetAllCultsAsync()
    {
        return await _cultRepository.GetAllAsync();
    }
}
