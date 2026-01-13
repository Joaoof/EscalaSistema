using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.Validation;

public class PublishScaleValidation: AbstractValidator<Scale>
{
    public PublishScaleValidation()
    {
        RuleFor(scale => scale.PublishedAt)
            .GreaterThan(DateTime.Now)
            .WithMessage("A data da escala deve ser futura.");
        RuleFor(scale => scale.IsPublished)
            .Equal(true)
            .WithMessage("A escala deve ser publicada.");
        RuleFor(scale => scale.IsPublished) .Equal(true)
            .WithMessage("A escala já está publicada.");
        RuleFor(scale => scale.IsPublished) .Equal(false)
            .WithMessage("A escala deve ser publicada.");
        RuleFor(scale => scale.Cult)
            .NotNull()
            .WithMessage("O culto associado à escala é obrigatório.");
        RuleFor(scale => scale.ScaleAssignments.Any())
            .NotEmpty()
            .Must(assignments => assignments.All(a => a.Musician != null))
            .WithMessage("Todas as músicas na escala devem ser válidas.");
        RuleFor(scale => scale.ScaleAssignments)
            .Must(assignments => assignments.All(a => a.MusicianId != Guid.Empty))
            .WithMessage("Todas as músicas na escala devem ter um músico associado.");
        RuleFor(scale => scale.IsClosed)
            .Equal(false)
            .WithMessage("A escala não pode estar fechada ao publicar.");
    }
}
