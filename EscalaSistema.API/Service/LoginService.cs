using DevOne.Security.Cryptography.BCrypt;
using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Middleware;
using EscalaSistema.API.Models;
using EscalaSistema.API.Repository.Login;

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
            throw new BadRequestException("Email ou senha inválidos");

        bool validPassword = BCryptHelper.CheckPassword(request.PasswordHash, user.PasswordHash);

        if (!validPassword)
            throw new BadRequestException("Email ou senha inválidos");

        if (!user.IsActive)
            throw new BadRequestException("Usuário desativado");

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Token = token
        };
    }
}

