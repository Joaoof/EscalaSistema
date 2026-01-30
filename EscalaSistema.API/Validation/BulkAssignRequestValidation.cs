using EscalaSistema.API.DTOs;
using FluentValidation;

namespace EscalaSistema.API.Validation;

public class BulkAssignRequestValidation : AbstractValidator<BulkAssignRequest>
{
    public BulkAssignRequestValidation()
    {
        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12)
            .WithMessage("O mês deve ser entre 1 e 12.");

        RuleFor(x => x.Year)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("O ano não pode ser no passado.");

        RuleFor(x => x.Assignments)
            .NotEmpty()
            .WithMessage("A lista de atribuições é obrigatória.");

        RuleForEach(x => x.Assignments).SetValidator(new MusicianBulkAssignmentValidation());
    }
}

public class MusicianBulkAssignmentValidation : AbstractValidator<MusicianBulkAssignment>
{
    public MusicianBulkAssignmentValidation()
    {
        RuleFor(x => x.MusicianId).NotEmpty().WithMessage("Músico inválido.");
        RuleFor(x => x.Role).IsInEnum().WithMessage("Instrumento inválido.");
        RuleFor(x => x.CultIds).NotEmpty().WithMessage("Selecione pelo menos um culto.");
    }
}