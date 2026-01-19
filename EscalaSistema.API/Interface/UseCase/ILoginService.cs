using EscalaSistema.API.DTOs.Login;

namespace EscalaSistema.API.Interface.UseCase;

public interface ILoginService
{
    Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest);  
}
