using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/scales")]
public class ScaleController : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Leader")]
    [Authorize(Policy = "CanPublishScale")]
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

    //[HttpPut]
    //[Authorize(Roles = "Leader")]
    //[Authorize(Policy = "CanPublishScale")]
    //public async Task<IActionResult> Update(Guid id, [FromBody] Models.Scale scale, [FromServices] ICreateScaleUseCase createScaleUseCase)
    //{
    //    var updatedScale = await createScaleUseCase.Update(id, scale);
    //    return Ok(new
    //    {
    //        updatedScale.Id,
    //        updatedScale.CultId,
    //        updatedScale.IsPublished,
    //        updatedScale.IsClosed,
    //    });
    //}

    //[HttpGet]
    //[Authorize(Roles = "Leader")]
    //[Authorize(Policy = "CanPublishScale")]
    //public async Task<IActionResult> Get([FromServices] ICreateScaleUseCase createScaleUseCase)
    //{
    //    var scales = await createScaleUseCase.Get();
    //    var result = scales.Select(scale => new
    //    {
    //        scale.Id,
    //        scale.CultId,
    //        scale.IsPublished,
    //        scale.IsClosed,
    //    });
    //    return Ok(result);
    //}

    [HttpPatch]
    [Route("scale/{id}/close")]
    [Authorize]
    public async Task<IActionResult> CloseScale(Guid id, [FromServices] ICreateScaleUseCase createScaleUseCase)
    {
        var closedScale = await createScaleUseCase.ScaleClosed(id);
        return Ok(closedScale);
    }

    [HttpPost]
    [Route("scale/bulk-assign")]
    //[Authorize(Roles = "Leader")] // Segurança
    public async Task<IActionResult> BulkAssign(
        [FromBody] BulkAssignRequest request,
        [FromServices] IBulkAssignUseCase bulkAssignUseCase)
    {
        await bulkAssignUseCase.Execute(request);
        return NoContent(); // Sucesso 204
    }
}
