using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface IPublishScaleRepository
{
    Task<Scale?> GetByIdAsync(Guid scaleId);
    Task SaveAsync();
}
