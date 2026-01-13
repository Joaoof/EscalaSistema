using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.Validation
{
    public class CultValidation: AbstractValidator<Cult>
    {
        public CultValidation()
        {
            RuleFor(cult => cult.Name)
                .NotEmpty().WithMessage("O nome do culto é obrigatório.")
                .MaximumLength(20).WithMessage("O nome do culto não pode exceder 20 caracteres.");
            RuleFor(cult => cult.DateTime).NotEmpty().WithMessage("A data e hora do culto são obrigatórias.");
        }
    }
}
