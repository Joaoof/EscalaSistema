namespace EscalaSistema.API.Domain.Errors;

public static class MusicErrors
{
    public static readonly DomainError MusicNotFound =
        new("MUSIC_001", "Música não encontrada", "A música não consta no banco de dados", 404);
    public static readonly DomainError InvalidTitleMusic =
        new("MUSIC_002", "Título da música inválido", "O título da música não pode ser vazio ou nulo", 400);
    public static readonly DomainError InvalidTitleMusicTooShort =
        new("MUSIC_003", "Título da música muito curto", "O título da música deve ter pelo menos 3 caracteres", 400);
    public static readonly DomainError InvalidTitleMusicTooLong =
        new("MUSIC_004", "Título da música muito longo", "O título da música deve ter no máximo 200 caracteres", 400);

    public static readonly DomainError MusicCount =
        new("MUSIC_005", "Número máximo de músicas atingido", "Não é possível adicionar mais músicas ao culto", 400);
}
