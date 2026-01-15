using EscalaSistema.API.Data;
using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.Repository;

public class AssignMusicianRepository : IAssignMusicianRepository
{
    private readonly EscalaSistemaDbContext _context;

    public AssignMusicianRepository(EscalaSistemaDbContext context)
    {
        _context = context;
    }

    public async Task AssignMusicianAsync(Guid scaleId, AssignMusicianToScaleRequest request)
    {
        await _context.ScaleAssignments.AddAsync(new ScaleAssignment { 
            ScaleId = scaleId,
            MusicianId = request.MusicianId,
            Role = request.Role,
            ConfirmationStatus = Enum.ConfirmationStatusEnum.Pending,
        });
        await _context.SaveChangesAsync();
    }
}
