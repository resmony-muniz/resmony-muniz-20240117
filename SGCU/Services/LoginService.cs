using Microsoft.IdentityModel.Tokens;
using SGCU.Data;
using SGCU.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGCU.Services;

public class LoginService : ILoginService
{
    private readonly IConfiguration _configuration;

    public LoginService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CriarToken(UsuarioReadDto usuario)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, usuario.Login)
        ];

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(2), signingCredentials: credenciais);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
