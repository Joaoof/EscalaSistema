using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.Enum;

namespace EscalaSistema.API.Models; 

public class Scale
{
    public Guid Id { get; set; }
    public Guid CultId { get; set; }
    public Cult Cult { get; set; }
    public ScaleConfirmedEnum Status { get; set; }
    public bool IsPublished { get; set; }
    public bool IsClosed { get; set; }
    public DateTime PublishedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<ScaleAssignment> ScaleAssignments { get; set; }

    public void Publish()
    {
        if (IsClosed)
            throw new DomainException(ScaleErrors.Closed);

        IsPublished = true;
        Status = ScaleConfirmedEnum.Published;
        PublishedAt = DateTime.UtcNow;
    }
    public void Touch()
    {
       UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        if (IsClosed)
            throw new DomainException(ScaleErrors.Closed);

        if (!IsPublished)
            throw new DomainException(ScaleErrors.CannotPublishEmptyScale);

        IsClosed = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAssignment(Musician musician, InstrumentEnum role)
    {
        // 1. Validações de Estado
        if (IsPublished || IsClosed)
            throw new DomainException(ScaleErrors.CannotModifyPublishedScale);

        // 2. Validações de Duplicidade
        if (ScaleAssignments.Any(x => x.MusicianId == musician.Id))
            throw new DomainException(ScaleErrors.UserAlreadyAssigned);

        // 3. Ação
        // Atenção: Aqui usamos o construtor protegido do ScaleAssignment que criamos antes
        // Se você ainda não protegeu o ScaleAssignment, use 'new ScaleAssignment { ... }'
        var assignment = new ScaleAssignment(this.Id, musician.Id, role);

        ScaleAssignments.Add(assignment);
        UpdatedAt = DateTime.UtcNow;
    }
}
