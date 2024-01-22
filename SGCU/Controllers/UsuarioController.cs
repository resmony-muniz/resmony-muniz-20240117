using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGCU.Data;
using SGCU.Models.DbSet;
using SGCU.Models.Dto;

namespace SGCU.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioController(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("Registrar")]
    public ActionResult<UsuarioReadDto> Registrar([FromBody] UsuarioCreateDto request)
    {
        var usuario = new UsuarioCreateDto()
        {
            Login = request.Login.Trim().ToLower(),
            Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Status = request.Status
        };

        var existeLogin = _context.Usuarios.Any(i => i.Login.Equals(usuario.Login.ToLower()));

        if (existeLogin)
            return BadRequest("Login não disponível");

        var user = _context.Usuarios.Add(_mapper.Map<UsuarioModel>(usuario));
        _context.SaveChanges();

        return user is not null
            ? CreatedAtAction(nameof(Registrar), user.Entity)
            : BadRequest("Não foi possível inserir o usuário");
    }

    [HttpPut("Atualizar/{id:guid}")]
    [Authorize]
    public ActionResult Atualizar([FromBody] UsuarioCreateDto request, Guid id)
    {
        var usuarioEncontrado = _context.Usuarios.FirstOrDefault(i => id.Equals(i.UsuarioId));

        if (usuarioEncontrado is null) return BadRequest("Usuário não encontrado");

        usuarioEncontrado.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
        usuarioEncontrado.Status = request.Status;

        _context.SaveChanges();

        return Ok(_mapper.Map<UsuarioReadDto>(usuarioEncontrado));
    }

    [HttpGet]
    [Route("ListarUsuarios/{status:bool?}")]
    [Authorize]
    public ActionResult<IEnumerable<UsuarioReadDto>> ObterUsuarios(bool? status = null) 
    {
        var usuarios = _context.Usuarios
                .Where(it => status == null || it.Status == status.Value)
                .Select(i => new { i.UsuarioId, i.Login, i.Status })
                .ToList();

        return Ok(usuarios);
    }
}
