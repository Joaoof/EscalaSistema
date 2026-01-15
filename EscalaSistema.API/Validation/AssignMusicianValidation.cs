using EscalaSistema.API.DTOs;
using FluentValidation;

namespace EscalaSistema.API.Validation;

public class AssignMusicianValidation: AbstractValidator<AssignMusicianToScaleRequest>
{
    public AssignMusicianValidation()
    {
        RuleFor(x => x.Role).IsInEnum()
            .WithMessage("O papel do músico é obrigatório e deve ser válido.");
        RuleFor(x => x.MusicianId)
            .NotEmpty()
            .WithMessage("O identificador do músico é obrigatório.");
    }
}
