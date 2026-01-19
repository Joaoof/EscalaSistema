using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using FluentValidation;

namespace EscalaSistema.API.UseCase;

public class UserRegisterUseCase: IUserRegisterUseCase
{
    private readonly IUserRegisterRepository _userRepository;
    private readonly IValidator<User> _validator;

    public UserRegisterUseCase(IUserRegisterRepository userRegisterRepository, IValidator<User> validator)
    {
        _userRepository = userRegisterRepository;
        _validator = validator;
    }

    public async Task<User> Execute(User user)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Role = user.Role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true

        };
        var validationResult = await _validator.ValidateAsync(newUser);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        await _userRepository.RegisterUserAsync(newUser);
        return newUser;
    }

    public async Task<List<User>> GetUser()
    {
        var getAll = await _userRepository.GetAllByUserAsync();

        return getAll;
    }
}
