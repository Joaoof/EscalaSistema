using EscalaSistema.API.DTOs.Login;
using FluentValidation;

namespace EscalaSistema.API.Validation.Login;

public class LoginValidation: AbstractValidator<LoginRequest>
{
    public LoginValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo Email é obrigatório.")
            .EmailAddress().WithMessage("O campo Email deve ser um endereço de email válido.");
        RuleFor(x => x.PasswordHash).NotEmpty().OverridePropertyName("PasswordHash").WithMessage("O campo Senha é obrigatório.");
    }
}
