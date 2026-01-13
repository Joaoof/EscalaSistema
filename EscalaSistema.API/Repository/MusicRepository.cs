using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Middleware;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Repository;

public class MusicRepository : IMusicRepository
{
    private readonly EscalaSistemaDbContext _context;

    public MusicRepository(EscalaSistemaDbContext context)
    {
        _context = context;
    }

    public async Task<Music> GetByIdAsync(Guid id)
    {
        return await _context.Musics.FindAsync(id);
    }

    public async Task<IEnumerable<Music>> GetAllAsync()
    {
        return await _context.Musics.ToListAsync();
    }

    public async Task AddAsync(Music music)
    {
        var getByCult = await _context.Cults.FindAsync(music.CultId) ?? throw new BadRequestException("Id não encontrado", 400);

        await _context.Musics.AddAsync(music);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var music = await GetByIdAsync(id);

        if (music != null)
        {
            _context.Musics.Remove(music);
            await _context.SaveChangesAsync();
        }
    }

}
