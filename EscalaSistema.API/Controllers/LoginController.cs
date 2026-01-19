using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using EscalaSistema.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/user")]
public class LoginController: ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, ILoginService loginService)
    {
        var result = await loginService.AuthenticateAsync(request);
        return Ok(result);
    }
}
