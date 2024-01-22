using System.ComponentModel.DataAnnotations;

namespace SGCU.Models.Dto;

public class UnidadeCreateDto
{
    private string _codigoUnidade;
    [StringLength(5)]
    public string CodigoUnidade { get => _codigoUnidade; set => _codigoUnidade = value.ToUpper(); }
    public string Nome { get; set; }
    public bool Status { get; set; }
}
