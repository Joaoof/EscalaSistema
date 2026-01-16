using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.UseCase;

public interface IUserRegisterUseCase
{
    Task Execute(User user);
}
