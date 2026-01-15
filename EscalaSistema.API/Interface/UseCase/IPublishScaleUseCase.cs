namespace EscalaSistema.API.Interface.UseCase;

public interface IPublishScaleUseCase
{
    Task ExecuteAsync(Guid scaleId);
}
