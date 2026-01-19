using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Models;

namespace EscalaSistema.API.Interface.Repository;

public interface ILoginRepository
{
    Task<User?> GetByEmailAsync(string email);
}
