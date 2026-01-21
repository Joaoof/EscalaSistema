namespace EscalaSistema.API.Domain.Errors;

public static class CultErrors
{
    public static readonly DomainError CultNotFound =
        new("CULT_001", "Culto não encontrado", "O culto não consta no banco de dados", 404);

    public static readonly DomainError DateTimeNotExists =
        new("CULT_002", "Data e hora do culto não existe", "Essa data não existe no banco de dados",400);

    public static readonly DomainError DateTimeExists = 
        new("CULT_003", "Já existe um culto cadastrado para essa data e hora", "Cadastro inválido. Duplicação do dados",400);

    public static readonly DomainError NotIsPublished =
        new("CULT_004", "O culto não está publicado", "Publicação falsa" ,400);

   public static readonly DomainError InvalidNameCult = 
        new("CULT_005", "Nome do culto inválido", "O nome do culto não pode ser vazio ou nulo",400);

    public static readonly DomainError InvalidDateTimeCannotFuture =
        new("CULT_006", "Data e hora inválida", "A data e hora do culto não pode ser no passado",400);
}
