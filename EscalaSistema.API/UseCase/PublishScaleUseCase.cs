using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class PublishScaleUseCase: IPublishScaleUseCase
{
    private readonly IPublishScaleRepository _publishScaleRepository;
    private readonly IValidator<CreateScaleRequest> _validator;

    public PublishScaleUseCase(IPublishScaleRepository publishScaleRepository, IValidator<CreateScaleRequest> validator)
    {
        _publishScaleRepository = publishScaleRepository;
        _validator = validator;
    }

    public async Task<Scale> Register (CreateScaleRequest scale)
    {
        var validationResult = await _validator.ValidateAsync(scale);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }   
        await _publishScaleRepository.PublishScaleAsync(scale.CultId);

        var result = new Scale
        {
            CultId = scale.CultId
        };

        return result;
    }
}
