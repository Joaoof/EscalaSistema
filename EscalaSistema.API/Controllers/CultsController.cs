using EscalaSistema.API.Data;
using EscalaSistema.API.Models;
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
    public IActionResult Create()
    {
        var cult = new Cult { 
            Id = Guid.NewGuid(),
            Name = "Cult Name",
            DateTime = DateTime.Now 
        };
        _db.Cults.Add(cult);
        _db.SaveChanges();

        return Ok(cult);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_db.Cults.ToList());
    }
}

