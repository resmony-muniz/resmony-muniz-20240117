namespace SGCU.Models.Dto;

public class ColaboradorReadDto
{
    public Guid ColaboradorId { get; set; }
    public string Nome { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid UnidadeId { get; set; }
}
