namespace EscalaSistema.API.Interface.UseCase;

public interface ITokenService
{
    string GenerateToken(Models.User user);
}
