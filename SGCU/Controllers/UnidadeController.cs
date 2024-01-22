using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SGCU.Data;
using SGCU.Models.DbSet;
using SGCU.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace SGCU.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UnidadeController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public UnidadeController(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("Registrar")]
    public ActionResult<UnidadeReadDto> Registrar([FromBody] UnidadeCreateDto request)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(request, new ValidationContext(request), results, true);

        EntityEntry<UnidadeModel>? unidade;

        try
        {
            unidade = _context.Unidades.Add(_mapper.Map<UnidadeModel>(request));
            _context.SaveChanges();
        }
        catch (Exception) 
        {
            return BadRequest("O código da unidade deve ser único.");
        }
        
        return
            unidade is not null
            ? CreatedAtAction(nameof(Registrar), _mapper.Map<UnidadeReadDto>(unidade.Entity))
            : BadRequest("Não foi possível registrar unidade");
    }

    [HttpPut("AlterarStatusUnidade/{id:guid}")]
    public ActionResult<UnidadeReadDto> InativarUnidade(Guid id)
    {
        var unidadeEncontrada = _context.Unidades.FirstOrDefault(i => id.Equals(i.UnidadeId));

        if (unidadeEncontrada is null) 
            return BadRequest("Unidade não encontrado");

        unidadeEncontrada.Status = !unidadeEncontrada.Status;

        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("ListarUnidades/{codUnidade?}")]
    public ActionResult<IEnumerable<UnidadeReadDto>> ListarUnidades(string? codUnidade = null)
    {
        var unidades = _context.Unidades
            .Where(it => codUnidade == null || it.CodigoUnidade == codUnidade.Trim().ToUpper())
            .Select(uni => _mapper.Map<UnidadeReadDto>(uni))
            .ToList();

        return Ok(unidades);
    }
}
