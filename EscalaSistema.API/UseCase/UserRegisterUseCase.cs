using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.UseCase;

public class UserRegisterUseCase: IUserRegisterUseCase
{
    private readonly IUserRegisterRepository _userRepository;

    public UserRegisterUseCase(IUserRegisterRepository userRegisterRepository)
    {
        _userRepository = userRegisterRepository;
    }

    public async Task Execute(User user)
    {
        await _userRepository.RegisterUserAsync(user);
    }
}
