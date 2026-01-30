namespace EscalaSistema.API.Domain.Errors;

public static class ScaleAssignmentErrors
{
    public static readonly DomainError GuidEmptyScaleAssignmentId =
        new("SCALE_ASSIGNMENT_001", "O ID da atribuição da escala não pode ser vazio.", "O ID fornecido é inválido.", 400);

    public static readonly DomainError ScaleAssignmentNotFound =
        new("SCALE_ASSIGNMENT_002", "A atribuição da escala não foi encontrada.", "Nenhuma atribuição correspondente ao ID fornecido.", 404);

    public static readonly DomainError GuidEmptyMusicAssignmentId =
        new("SCALE_ASSIGNMENT_003", "O ID da atribuição de música não pode ser vazio.", "O ID fornecido é inválido.", 400);

    public static readonly DomainError IsEnumInvalid =
        new("SCALE_ASSIGNMENT_004", "O valor do enum fornecido é inválido.", "O valor do enum não corresponde a nenhum valor definido.", 400);

}
