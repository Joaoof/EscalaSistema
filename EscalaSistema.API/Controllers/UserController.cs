using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user, [FromServices] IUserRegisterUseCase userRegisterUseCase)
    {
        await userRegisterUseCase.Execute(user);
        return Ok();
    }
}