using EscalaSistema.API.DTOs;

namespace EscalaSistema.API.Interface.UseCase;

public interface IBulkAssignUseCase
{
    Task Execute(BulkAssignRequest request);
}
