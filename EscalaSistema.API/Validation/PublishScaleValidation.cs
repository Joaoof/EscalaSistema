using EscalaSistema.API.DTOs;
using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.Validation;

// C#
public class PublishScaleValidation : AbstractValidator<PublishScaleRequest>
{
    public PublishScaleValidation()
    {
        RuleFor(r => r.ScaleId)
            .NotEmpty()
            .WithMessage("O identificador da escala é obrigatório.");
    }
}