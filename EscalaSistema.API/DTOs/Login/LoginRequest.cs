using EscalaSistema.API.Middleware;
using System.Text.RegularExpressions;

namespace EscalaSistema.API.DTOs.Login;

public class LoginRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    //public LoginRequest(string email, string passwordHash)
    //{
    //    if(passwordHash.Length < 8 || passwordHash.Length > 16) 
    //        throw new BadRequestException("A senha deve ter entre 8 e 16 caracteres");

    //    Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]+");

    //    if (regex.Match(passwordHash).Value == "")
    //        throw new BadRequestException("A senha deve conter um número e um caractere especial");
    //}
}
