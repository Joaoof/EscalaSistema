namespace EscalaSistema.API.Domain.Errors;

public static class ScaleErrors
{
    public static readonly DomainError AlreadyExists =
        new("SCALE_001", "Já existe uma escala para este culto", "Já existe uma escala para este culto", 409);

    public static readonly DomainError Closed =
        new("SCALE_002", "Escala já está fechada", "Escala já está fechada", 400);

    public static readonly DomainError NotFound = 
        new("SCALE_003", "Escala não encontrada", "Escala não encontrada", 404);

    public static readonly DomainError CannotPublishEmptyScale =
        new("SCALE_004", "Não é possível publicar uma escala vazia", "Não é possível publicar uma escala vazia", 400);

    public static readonly DomainError UserNotAvailable =
        new("SCALE_005", "Usuário não está disponível para esta data", "Usuário não está disponível para esta data", 400);

    public static readonly DomainError UserAlreadyAssigned =
        new("SCALE_006", "Usuário já está atribuído a esta função na escala", "Usuário já está atribuído a esta função na escala", 400);

    public static readonly DomainError InvalidDate =
        new("SCALE_007", "Data inválida para a escala", "Data inválida para a escala", 400);

    public static readonly DomainError CannotModifyPublishedScale =
        new("SCALE_008", "Não é possível modificar uma escala publicada", "Não é possível modificar uma escala publicada", 400);

    public static readonly DomainError RoleNotFound =
        new("SCALE_009", "Função não encontrada na escala", "Função não encontrada na escala", 404);

    public static readonly DomainError InsufficientPermissions =
        new("SCALE_010", "Permissões insuficientes para modificar a escala", "Permissões insuficientes para modificar a escala", 403);

    public static readonly DomainError IsNotScaleAssignments =
        new("SCALE_011", "Nenhum músico atribuído a esta escala", "Nenhum músico atribuído a esta escala", 404);
}