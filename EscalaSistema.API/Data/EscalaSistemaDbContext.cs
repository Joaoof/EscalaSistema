using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Data;

public class EscalaSistemaDbContext: DbContext
{
    public DbSet<Cult> Cults => Set<Cult>();
    public DbSet<Musician> Musicians => Set<Musician>();
    public DbSet<Scale> Scales => Set<Scale>();
    public DbSet<ScaleAssignment> ScaleAssignments => Set<ScaleAssignment>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Music> Musics => Set<Music>();

    public EscalaSistemaDbContext(DbContextOptions<EscalaSistemaDbContext> options): base(options)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Music>()
            .HasOne(c => c.Cult)
            .WithMany(c => c.Musics)
            .HasForeignKey(c => c.CultId)
            .IsRequired();

        modelBuilder.Entity<Scale>()
            .HasOne(c => c.Cult)
            .WithOne(c =>c.Scale)
            .HasForeignKey<Scale>(c => c.CultId)
            .IsRequired();

        modelBuilder.Entity<ScaleAssignment>()
            .HasOne(s => s.Scale)
            .WithMany(s => s.ScaleAssignments)
            .HasForeignKey(s => s.ScaleId)
            .IsRequired();

        modelBuilder.Entity<ScaleAssignment>()
            .HasOne(sa => sa.Musician)
            .WithMany(m => m.ScaleAssignments)
            .HasForeignKey(sa => sa.MusicianId)
            .IsRequired();
    }
}
