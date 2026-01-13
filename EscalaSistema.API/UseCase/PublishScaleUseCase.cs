using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class PublishScaleUseCase
{
    private readonly IPublishScaleRepository _publishScaleRepository;
    private readonly IValidator<Scale> validator;
}
