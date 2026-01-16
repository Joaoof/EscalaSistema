namespace EscalaSistema.API.DTOs.Login;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Email { get; set; } 
    public string PasswordHash { get; set; }
}
