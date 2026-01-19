using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Repository.Login;

public class LoginRepository: ILoginRepository
{
    private readonly EscalaSistemaDbContext _context;

    public LoginRepository(EscalaSistemaDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
