using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface IUserRegisterRepository
{
    Task<bool> IsEmailRegisteredAsync(string email);
    Task<bool> IsEmailRegisteredAsync(Guid userId, string email);
    Task<User> RegisterUserAsync(User user);
    Task<List<User>> GetAllByUserAsync();
}
