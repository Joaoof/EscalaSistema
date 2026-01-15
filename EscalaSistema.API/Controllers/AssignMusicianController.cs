using EscalaSistema.API.DTOs;
using EscalaSistema.API.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/scales")]
public class AssignMusicianController: ControllerBase
{
    [HttpPost("{scaleId}/assignments")]
    public async Task<IActionResult> AssignMusician(
    Guid scaleId,
    [FromBody] AssignMusicianToScaleRequest request, [FromServices] AssignMusicianUseCase assignMusicianUseCase)
    {
        await assignMusicianUseCase.Register(scaleId, request);
        return NoContent();
    }

}
