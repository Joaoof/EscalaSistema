namespace EscalaSistema.API.DTOs.Login;

public class LoginRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
