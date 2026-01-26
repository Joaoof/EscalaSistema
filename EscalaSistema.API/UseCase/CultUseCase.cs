using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class CultUseCase : ICultUseCase
{
    private readonly ICultRepository _cultRepository;
    private readonly IValidator<CultRequest> _validator;
    public CultUseCase(ICultRepository cultRepository, IValidator<CultRequest> validator)
    {
        _cultRepository = cultRepository;
        _validator = validator;
    }

    public async Task<CultResponse> Register(CultRequest cult)
    {
        var register = new Cult(cult.Name, cult.DateTime);

        var validationResult = await _validator.ValidateAsync(cult);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _cultRepository.AddAsync(cult);

        return new CultResponse
        {
            Name = register.Name,
            Date = register.DateTime,
            IsPublished = false,
            IsClosed = false
        };

    }

    public async Task<IEnumerable<Cult>> GetAllCultsAsync()
    {
        return await _cultRepository.GetAllAsync();
    }
}
