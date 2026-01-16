using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using System.Security.AccessControl;

namespace EscalaSistema.API.Repository.Login;

public class UserRegisterRepository : IUserRegisterRepository
{
    private readonly EscalaSistemaDbContext _context;

    public UserRegisterRepository(EscalaSistemaDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEmailRegisteredAsync(string email)
    {
        return await Task.FromResult(_context.Users.Any(u => u.Email == email));
    }

    public Task<bool> IsEmailRegisteredAsync(Guid userId, string email)
    {
        throw new NotImplementedException();
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == email));
    }

    public async Task<User> GetAllByUserAsync()
    {
        return await Task.FromResult(_context.Users.FindAsync())
    }
}
