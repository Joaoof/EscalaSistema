using EscalaSistema.API.DTOs;
using FluentValidation;

namespace EscalaSistema.API.Validation
{
    public class ScaleValidation : AbstractValidator<ScaleRequest>
    {
        public ScaleValidation()
        {
            RuleFor(x => x.CultId)
            .NotEmpty()
            .WithMessage("CultId é obrigatório");
        }
    }
}
