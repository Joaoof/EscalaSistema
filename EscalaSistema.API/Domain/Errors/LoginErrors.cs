namespace EscalaSistema.API.Domain.Errors;

public static class LoginErrors
{
    public static readonly DomainError InvalidCredentials =
        new("LOGIN_001", "Credenciais inválidas", "Email ou senha incorretos", 401);

    public static readonly DomainError InvalidPassword = 
        new("LOGIN_002", "Senha inválida", "A senha deve ter entre 8 e 16 caracteres, conter ao menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial", 400);
    public static readonly DomainError UserInactive =
        new("LOGIN_003", "Usuário inativo", "O usuário está desativado", 403);
    public static readonly DomainError UserNotFound =
        new("LOGIN_004", "Usuário não encontrado", "O usuário solicitado não foi encontrado", 404);

    public static readonly DomainError UserNotAuthenticated =
        new("LOGIN_005", "Usuário não autenticado", "O usuário não está autenticado", 401);
}
