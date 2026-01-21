using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/cults")]
public class CultController : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CultRequest cut, [FromServices] ICultUseCase cultUseCase)
    {
        var cult = await cultUseCase.Register(cut);

        return Ok(cult);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromServices] ICultUseCase cultUseCase)
    {
        var cults = await cultUseCase.GetAllCultsAsync();

        return Ok(cults);
    }
}

