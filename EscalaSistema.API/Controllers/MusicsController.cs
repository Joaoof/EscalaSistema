using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscalaSistema.API.Controllers;

[ApiController]
[Route("api/musics")]
public class MusicsController: ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMusicRequest musicResponse, [FromServices] IMusicUseCase musicUseCase)
    {
        var music = await musicUseCase.Register(musicResponse);
        return Ok(music);
    }
}
