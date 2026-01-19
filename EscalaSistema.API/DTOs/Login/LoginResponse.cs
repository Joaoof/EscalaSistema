namespace EscalaSistema.API.DTOs.Login;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}

