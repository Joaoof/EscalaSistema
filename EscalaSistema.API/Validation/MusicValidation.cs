using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.Validation;

public class MusicValidation: AbstractValidator<Music>
{
    public MusicValidation()
    {
        RuleFor(music => music.Name)
            .NotEmpty().WithMessage("O título da música é obrigatório.")
            .MaximumLength(200).WithMessage("O título da música não pode exceder 200 caracteres.");
        RuleFor(music => music.Key).NotEmpty().WithErrorCode("A tonalidade da música é obrigatória.").WithMessage("A tonalidade da música é obrigatória.");
        RuleFor(music => music.YoutubeUrl)
            .NotEmpty().WithMessage("A URL do YouTube é obrigatória.")
            .MaximumLength(200).WithMessage("A URL do YouTube não pode exceder 200 caracteres.");
    }
}
