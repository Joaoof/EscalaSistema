using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/publish")]

public class PublishScaleController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ScaleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateScaleRequest request, [FromServices] IPublishScaleUseCase publishScaleUseCase)
    {
        var scale = await publishScaleUseCase.Register(request);

        return Ok(new ScaleResponse
        {
            Id = scale.Id,
            CultId = scale.CultId,
            IsPublished = scale.IsPublished,
            IsClosed = scale.IsClosed,
            PublishedAt = scale.PublishedAt,
            UpdatedAt = scale.UpdatedAt,
            AssignmentsCount = scale.ScaleAssignments?.Count ?? 0
        });
    }
}
