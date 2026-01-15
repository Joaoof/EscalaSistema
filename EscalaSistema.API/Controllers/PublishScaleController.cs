using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/publish")]

public class PublishScaleController : ControllerBase
{
    [HttpPost("{scaleId}/publish")]
    [ProducesResponseType(typeof(ScaleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(Guid scaleId, [FromServices] IPublishScaleUseCase publishScaleUseCase)
    {
        await publishScaleUseCase.ExecuteAsync(scaleId);
        return NoContent();
    }
}
