using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.UseCase;

public interface IUserRegisterUseCase
{
    Task<User> Execute(User user);

    Task<List<User>> GetUser();
}
