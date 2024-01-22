using SGCU.Models.Dto;

namespace SGCU.Services;

public interface ILoginService
{
    string CriarToken(UsuarioReadDto usuario);
}