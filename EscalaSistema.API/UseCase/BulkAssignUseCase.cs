using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.UseCase;

public class BulkAssignUseCase : IBulkAssignUseCase
{
    private readonly ICreateScaleRepository _scaleRepo;
    private readonly IMusicianRepository _musicianRepo;
    private readonly ICultRepository _cultRepo;

    public BulkAssignUseCase(
        ICreateScaleRepository scaleRepo,
        IMusicianRepository musicianRepo,
        ICultRepository cultRepo)
    {
        _scaleRepo = scaleRepo;
        _musicianRepo = musicianRepo;
        _cultRepo = cultRepo;
    }

    public async Task Execute(BulkAssignRequest request)
    {
        var allCultIds = request.Assignments
        .SelectMany(a => a.CultIds)
        .Distinct()
        .ToList();

        var existingScales = await _scaleRepo.GetByCultIdsAsync(allCultIds);

        var musicianIds = request.Assignments.Select(a => a.MusicianId).Distinct().ToList();
        var musicians = await _musicianRepo.GetByIdsAsync(musicianIds);

        foreach (var assignment in request.Assignments)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == assignment.MusicianId);
            if (musician == null)
                throw new DomainException(MusicErrors.MusicNotFound);

            foreach (var cultId in assignment.CultIds)
            {
                var scale = existingScales.FirstOrDefault(s => s.CultId == cultId);

                if (scale == null)
                {
                    var cult = await _cultRepo.GetByIdASync(cultId);
                    if (cult == null)
                        throw new DomainException(CultErrors.CultNotFound);

                    scale = new Scale
                    {
                        Id = Guid.NewGuid(),
                        CultId = cultId,
                        IsPublished = false,
                        IsClosed = false,
                        ScaleAssignments = new List<ScaleAssignment>()
                    };

                    _scaleRepo.Add(scale);

                    existingScales.Add(scale);
                }

                var alreadyAssigned = scale.ScaleAssignments
                    .Any(sa => sa.MusicianId == musician.Id);

                if (!alreadyAssigned)
                {
                    var newAssignment = new ScaleAssignment(scale.Id, musician.Id, assignment.Role);

                    scale.ScaleAssignments.Add(newAssignment);
                }
            }
        }

        await _scaleRepo.SaveChangesAsync();
    }
}