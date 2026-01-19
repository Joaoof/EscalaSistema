using EscalaSistema.API.Interface.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/scales")]
public class ScaleController: ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Guid cultId, [FromServices] ICreateScaleUseCase createScaleUseCase)
    {
        var scale = await createScaleUseCase.Register(cultId);

        return Ok(new
        {
            scale.Id,
            scale.CultId,
            scale.IsPublished,
            scale.IsClosed,
        });
        
    }
}
