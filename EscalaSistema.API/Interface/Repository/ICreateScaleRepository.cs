using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface ICreateScaleRepository
{
    Task<Scale> CreateAsync(Guid cultId);
    Task<List<Scale>> GetByMonthAsync(int month, int year);
    Task CloseScale();
    Task SaveChangesAsync();
    void Add(Scale scale);
    Task<Scale?> GetByIdAsync(Guid id);
    // Adicione esta assinatura
    Task<List<Scale>> GetByCultIdsAsync(List<Guid> cultIds);
}
