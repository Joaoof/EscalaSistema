using EscalaSistema.API.Enum;

namespace EscalaSistema.API.DTOs;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public UserRoleEnum Role { get; set; }
}