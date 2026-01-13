namespace EscalaSistema.API.Interface.Repository;

public interface IPublishScaleRepository
{
    Task PublishScaleAsync(Guid scaleId);
}
