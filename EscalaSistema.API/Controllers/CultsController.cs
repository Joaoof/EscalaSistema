using EscalaSistema.API.Data;
using EscalaSistema.API.DTOs;
using EscalaSistema.API.Models;
using EscalaSistema.API.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cults")]
public class CultController : ControllerBase
{
    private readonly EscalaSistemaDbContext _db;

    public CultController(EscalaSistemaDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CultResponse cut, [FromServices] IValidator<Cult> validator)
    {
        var cult = new Cult
        {
            Id = Guid.NewGuid(),
            Name = cut.Name,
            DateTime = cut.Date
        };
        var validationResult = await validator.ValidateAsync(cult);

        if (!validationResult.IsValid)
        {
            // Retorna HTTP 400 com a lista de erros
            return BadRequest(validationResult.Errors);
        }
        _db.Cults.Add(cult);
        await _db.SaveChangesAsync();

        return Ok(cult);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_db.Cults.ToList());
    }
}

