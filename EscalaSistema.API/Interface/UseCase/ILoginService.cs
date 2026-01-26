using EscalaSistema.API.DTOs;
using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Models;
using System.Security.Claims;

namespace EscalaSistema.API.Interface.UseCase;

public interface ILoginService
{
    Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest);
    Task<UserResponse> GetCurrentUserInfo(ClaimsPrincipal userPrincipal);
}
