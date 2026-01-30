using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.Enum;

namespace EscalaSistema.API.Models;

public class ScaleAssignment
{
    public Guid Id { get; private set; }
    public Guid ScaleId { get; internal set; }
    public Scale Scale { get; private set; } // EF Core
    public Guid MusicianId { get; internal set; }
    public Musician Musician { get; private set; } // EF Core

    public InstrumentEnum Role { get; internal set; }
    public ConfirmationStatusEnum ConfirmationStatus { get; internal set; }
    public DateTime ConfirmedAt { get; private set; }

    protected ScaleAssignment() { }

    public ScaleAssignment(Guid scaleId, Guid musicianId, InstrumentEnum role)
    {
        if (scaleId == Guid.Empty)
            throw new DomainException(ScaleAssignmentErrors.GuidEmptyScaleAssignmentId);

        if (musicianId == Guid.Empty)
            throw new DomainException(ScaleAssignmentErrors.GuidEmptyMusicAssignmentId);

        if (!System.Enum.IsDefined(typeof(InstrumentEnum), role))
            throw new DomainException(ScaleAssignmentErrors.IsEnumInvalid);

        Id = Guid.NewGuid();
        ScaleId = scaleId;
        MusicianId = musicianId;
        Role = role;
        ConfirmationStatus = ConfirmationStatusEnum.Pending;
        ConfirmedAt = DateTime.MinValue;
    }

    public void Confirmed()
    {
        ConfirmationStatus = ConfirmationStatusEnum.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
    }

    public void Recused()
    {
        ConfirmationStatus = ConfirmationStatusEnum.Declined;
        ConfirmedAt = DateTime.UtcNow;
    }
}