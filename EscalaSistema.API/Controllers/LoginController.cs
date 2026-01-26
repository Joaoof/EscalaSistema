using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Interface.UseCase;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    [Route("/api/auth/me")]
    [Authorize(Roles = "Leader")]
    [Authorize(Policy = "CanPublishScale")]
    public async Task<IActionResult> GetCurrentUserInfo([FromServices] ILoginService loginService)
    {
        var user = await loginService.GetCurrentUserInfo(User);
        return Ok(new
        {
            user.Id,
            user.Username,
            user.Role
        });
    }

}
