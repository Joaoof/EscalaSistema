namespace EscalaSistema.API.Domain.Errors;

public static class AuthErrors
{
    public static readonly DomainError NotAuthenticated =
        new("AUTH_001", "Usuário não autenticado", "Usuário não permitido para publicar escala", 401);

    public static readonly DomainError NotAllowedToPublishScale =
        new("AUTH_002", "Usuário não permitido para publicar escala", "Somente lideres podem publicar as escalas do mês", 403);

    public static readonly DomainError NotAllowedToModifyScale =
        new("AUTH_003", "Usuário não permitido para modificar escala", "Somente líderes podem modificar a escala",403);

    public static readonly DomainError NotValid = 
        new("AUTH_004", "Autenticação inválida", "As credenciais fornecidas são inválidas", 401);
}
