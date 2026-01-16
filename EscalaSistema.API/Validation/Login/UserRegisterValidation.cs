using EscalaSistema.API.DTOs.Login;
using FluentValidation;

namespace EscalaSistema.API.Validation.Login;

public class UserRegisterValidation: AbstractValidator<RegisterRequest>
{
    public UserRegisterValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("O campo Nome de Usuário é obrigatório.")
            .MinimumLength(3).WithMessage("O campo Nome de Usuário deve ter no mínimo 3 caracteres.")
            .MaximumLength(50).WithMessage("O campo Nome de Usuário deve ter no máximo 50 caracteres.");
        RuleFor(x => x.Email).EmailAddress()
            .NotEmpty().WithMessage("O campo Email é obrigatório.")
            .EmailAddress().WithMessage("O campo Email deve ser um endereço de email válido.");
        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("O campo Senha é obrigatório.");
    }
}
