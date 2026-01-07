using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Data;

public class EscalaSistemaDbContext: DbContext
{
    public DbSet<Cult> Cults => Set<Cult>();

    public EscalaSistemaDbContext(DbContextOptions<EscalaSistemaDbContext> options): base(options)
    {
       
    }
}
