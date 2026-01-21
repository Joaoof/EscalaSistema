using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/musicians")]
public class MusiciansController: ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateMusicianRequest request, [FromServices] ICreateMusicianUseCase createMusicianUseCase)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized("Token sem identificação de usuário.");
    
        var userId = Guid.Parse(userIdClaim);

        var response = await createMusicianUseCase.Execute(request, userId);

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }
}
