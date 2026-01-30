using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Middleware;

namespace EscalaSistema.API.UseCase;

public class PublishScaleUseCase : IPublishScaleUseCase
{
    private readonly IPublishScaleRepository _publishScaleRepository;

    public PublishScaleUseCase(IPublishScaleRepository publishScaleRepository)
    {
        _publishScaleRepository = publishScaleRepository;
    }

    public async Task ExecuteAsync(Guid scaleId)
    {
        var scale = await _publishScaleRepository.GetByIdAsync(scaleId);

        if (scale is null)
            throw new DomainException(ScaleErrors.NotFound);

        if (scale.IsClosed)
            throw new DomainException(ScaleErrors.Closed);

        if (scale.IsPublished)
            throw new DomainException(ScaleErrors.AlreadyExists);

        if (!scale.ScaleAssignments.Any())
            throw new DomainException(ScaleErrors.IsNotScaleAssignments);

        scale.Publish();

        await _publishScaleRepository.SaveAsync();
    }
}