using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGCU.Data;
using SGCU.Models.DbSet;
using SGCU.Models.Dto;

namespace SGCU.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ColaboradorController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public ColaboradorController(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("Registrar")]
    public ActionResult<ColaboradorReadDto> Registrar([FromBody] ColaboradorCreateDto request)
    {
        var unidadeAtiva = _context.Unidades.FirstOrDefault(it => it.Status && it.UnidadeId == request.UnidadeId);

        if (unidadeAtiva is null)
            return BadRequest("Não há unidade ativa com o Id informado");

        var usuarioEncontrado = _context.Usuarios.FirstOrDefault(it => it.UsuarioId == request.UsuarioId);

        if (usuarioEncontrado is null)
            return BadRequest("Não há usuário com o Id informado");

        var colaborador = _context.Colaboradores.Add(_mapper.Map<ColaboradorModel>(request));

        _context.SaveChanges();

        return colaborador is not null
            ? CreatedAtAction(nameof(Registrar), _mapper.Map<ColaboradorReadDto>(colaborador.Entity))
            : BadRequest("Não foi possível registrar colaborador");
    }

    [HttpDelete("Remover/{id:guid}")]
    public ActionResult Remover(Guid id)
    {
        var colaborador = _context.Colaboradores.FirstOrDefault(it => it.ColaboradorId == id);

        if (colaborador is null)
            return NotFound("Colaborador não encontrado");

        _context.Colaboradores.Remove(colaborador);
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("ListarColaboradoresPorUnidade/{codUnidade}")]
    public ActionResult<IEnumerable<ColaboradorReadDto>> ListarColaboradoresPorUnidade(string codUnidade)
    {
        var colaboradores = _context.Colaboradores
            .Join(_context.Unidades, uni => uni.UnidadeId, colab => colab.UnidadeId, (c, u) => new { Colaboradores = c, Unidades = u})
            .Where(it => it.Unidades.CodigoUnidade == codUnidade.ToUpper())
            .Select(c => _mapper.Map<ColaboradorReadDto>(c.Colaboradores))
            .ToList();

        return Ok(colaboradores);
    }

    [HttpGet("ListarColaboradores")]
    public ActionResult<IEnumerable<ColaboradorReadDto>> ListarColaboradores()
    {
        var colaboradores = _context.Colaboradores
            .Select(c => _mapper.Map<ColaboradorReadDto>(c))
            .ToList();

        return Ok(colaboradores);
    }

    [HttpPut("AlterarColaborador/{id:guid}")]
    public ActionResult<UnidadeReadDto> InativarUnidade(Guid id, [FromBody] ColaboradorReadDto colaborador)
    {
        var colabEncontrado = _context.Colaboradores.FirstOrDefault(i => id.Equals(i.ColaboradorId));

        if (colabEncontrado is null)
            return BadRequest("Colaborador não encontrado");

        colabEncontrado.Nome = colaborador.Nome.Trim();
        colabEncontrado.UnidadeId = colaborador.UnidadeId;

        _context.SaveChanges();

        return Ok();
    }
}
