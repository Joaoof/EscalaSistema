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

        if (scale == null)
            throw new NotFoundException("Escala não encontrada");

        if (scale.IsClosed)
            throw new BadRequestException("Escala encerrada não pode ser publicada");

        if (scale.IsPublished)
            throw new BadRequestException("Escala já publicada");

        if (!scale.ScaleAssignments.Any())
            throw new BadRequestException("Escala sem músicos não pode ser publicada");

        scale.Publish();

        await _publishScaleRepository.SaveAsync();
    }
}