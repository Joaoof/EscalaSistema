using EscalaSistema.API.DTOs;
using FluentValidation;

namespace EscalaSistema.API.Validation;

public class CultRequestValidation: AbstractValidator<CultRequest>
{
    public CultRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("O nome não pode ser vazio");
        RuleFor(x => x.DateTime).NotEmpty().WithMessage("A data e hora não podem ser vazias");
    }
}
