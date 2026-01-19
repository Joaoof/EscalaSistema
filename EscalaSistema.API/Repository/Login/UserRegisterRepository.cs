using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Repository.Login;

public class UserRegisterRepository : IUserRegisterRepository
{
    private readonly EscalaSistemaDbContext _context;

    public string Password { get; private set; }

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
        var salt = DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt();
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = user.Email,
            Username = user.Username,
            Role = user.Role,
            PasswordHash = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(user.PasswordHash, salt),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == email));
    }

    public async Task<List<User>> GetAllByUserAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }
}
