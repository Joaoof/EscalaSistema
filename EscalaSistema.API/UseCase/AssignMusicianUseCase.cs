using EscalaSistema.API.DTOs;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class AssignMusicianUseCase: IAssignMusicianUseCase
{
    private readonly IAssignMusicianRepository _assignMusicianRepository;
    private readonly IValidator<AssignMusicianToScaleRequest> _validator;

    public AssignMusicianUseCase(IAssignMusicianRepository assignMusicianRepository, IValidator<AssignMusicianToScaleRequest> validator)
    {
        _assignMusicianRepository = assignMusicianRepository;
        _validator = validator;
    }

    public async Task Register(Guid scaleId, AssignMusicianToScaleRequest assignMusicianToScaleRequest)
    {
        var validationResult = await _validator.ValidateAsync(assignMusicianToScaleRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        await _assignMusicianRepository.AssignMusicianAsync(scaleId, assignMusicianToScaleRequest);
    }
}
