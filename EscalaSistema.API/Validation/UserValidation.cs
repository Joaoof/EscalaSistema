using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.Validation;

public class UserValidation: AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username é obrigatório.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido.");
        RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Senha é obrigatória.");
    }
}
