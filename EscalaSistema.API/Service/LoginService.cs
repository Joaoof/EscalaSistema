using DevOne.Security.Cryptography.BCrypt;
using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.DTOs;
using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Enum;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Middleware;
using System.Security.Claims;

namespace EscalaSistema.API.Service;

public class LoginService : ILoginService
{
    private readonly ITokenService _tokenService;
    private readonly ILoginRepository _loginRepository;

    public LoginService(ITokenService tokenService, ILoginRepository loginRepository)
    {
        _tokenService = tokenService;
        _loginRepository = loginRepository;
    }

    public async Task<LoginResponse> AuthenticateAsync(LoginRequest request)
    {
        var user = await _loginRepository.GetByEmailAsync(request.Email);

        if (user == null)
            throw new DomainException(AuthErrors.NotValid);

        bool validPassword = BCryptHelper.CheckPassword(request.PasswordHash, user.PasswordHash);

        if (!validPassword)
            throw new DomainException(AuthErrors.NotValid);

        if (!user.IsActive)
            throw new DomainException(AuthErrors.NotValid);

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Token = token
        };
    }

    public async Task<UserResponse> GetCurrentUserInfo(ClaimsPrincipal userPrincipal)
    {
        var userIdStr = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = userPrincipal.Identity?.Name;
        var roleStr = userPrincipal.FindFirst(ClaimTypes.Role)?.Value;

        Guid userId = Guid.Empty;
        if (!string.IsNullOrEmpty(userIdStr))
        {
            Guid.TryParse(userIdStr, out userId);
        }

        // Conversão segura do Enum
        UserRoleEnum role = UserRoleEnum.Musician; // Valor padrão
        // A Entidade User tem regras de negócio e construtores fechados.
        // O DTO é apenas uma caixinha de dados aberta.
        return new UserResponse
        {
            Id = userId,
            Username = username,
            Role = role,
        };
    }
}

