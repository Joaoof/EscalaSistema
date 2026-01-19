using EscalaSistema.API.Auth;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EscalaSistema.API.Service
{   
    public class TokenService: ITokenService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.JwtSecretKey);

            var claims = new[]
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                new System.Security.Claims.Claim("role", user.Role.ToString()),
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
