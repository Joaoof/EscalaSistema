using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/musicians")]
public class MusiciansController: ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMusicianRequest request, [FromServices] ICreateMusicianUseCase createMusicianUseCase)
    {
        var userId = Guid.Parse(User.FindFirst("userId").Value ?? Guid.Empty.ToString());

        var response = await createMusicianUseCase.Execute(request, userId);

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }
}
