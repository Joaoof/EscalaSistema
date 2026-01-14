using EscalaSistema.API.DTOs;
using FluentValidation;

namespace EscalaSistema.API.Validation
{
    public class CreatePublishScaleValidation : AbstractValidator<CreateScaleRequest>
    {
        public CreatePublishScaleValidation()
        {
            RuleFor(x => x.CultId)
            .NotEmpty()
            .WithMessage("CultId é obrigatório");
        }
    }
}
