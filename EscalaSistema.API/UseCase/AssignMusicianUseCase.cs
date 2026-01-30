using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class AssignMusicianUseCase : IAssignMusicianUseCase
{
    // 1. Mudamos as dependências: Não usamos mais aquele repositório "tapa-buraco"
    private readonly ICreateScaleRepository _scaleRepository;
    private readonly IMusicianRepository _musicianRepository;
    private readonly IValidator<AssignMusicianToScaleRequest> _validator;

    public AssignMusicianUseCase(
        ICreateScaleRepository scaleRepository,
        IMusicianRepository musicianRepository,
        IValidator<AssignMusicianToScaleRequest> validator)
    {
        _scaleRepository = scaleRepository;
        _musicianRepository = musicianRepository;
        _validator = validator;
    }

    public async Task Register(Guid scaleId, AssignMusicianToScaleRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var scale = await _scaleRepository.GetByIdAsync(scaleId);

        if (scale == null)
            throw new DomainException(ScaleErrors.NotFound);

        var musician = await _musicianRepository.GetByIdAsync(request.MusicianId);

        if (musician == null)
            throw new DomainException(ScaleErrors.UserNotAvailable); // Ou erro específico "Músico não encontrado"

        scale.AddAssignment(musician, request.Role);
        //await _scaleRepository.UpdateAsync(scale.Id, scale);
    }
}