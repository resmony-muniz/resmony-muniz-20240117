using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGCU.Data;
using SGCU.Models.Dto;
using SGCU.Services;

namespace SGCU.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorizacaoController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly ApiDbContext _dataContext;
    private readonly IMapper _mapper;

    public AutorizacaoController(ILoginService loginService, ApiDbContext dataContext, IMapper mapper)
    {
        _loginService = loginService;
        _dataContext = dataContext;
        _mapper = mapper;
    }

    [HttpPost("Login")]
    public ActionResult Login(UsuarioLoginDto login)
    {
        var usuarioEncontrado = _dataContext.Usuarios.FirstOrDefault(it => login.Login.ToLower().Equals(it.Login));

        Console.WriteLine($"Usuário encontrado: {usuarioEncontrado?.Login}, senha: {login.Senha}, {usuarioEncontrado?.Senha}");

        if (usuarioEncontrado == null || !BCrypt.Net.BCrypt.Verify(login.Senha, usuarioEncontrado.Senha, hashType: BCrypt.Net.HashType.SHA512))
            return BadRequest(new { Mesage = "Usuário ou senha informados são inválidos." });

        var usr = _mapper.Map<UsuarioReadDto>(usuarioEncontrado);
        string token = _loginService.CriarToken(usr);

        return Ok(new { Usuario = usr, Token = token });
    }
}
