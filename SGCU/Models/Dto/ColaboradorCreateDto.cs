namespace SGCU.Models.Dto;

public class ColaboradorCreateDto
{
    public string Nome { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid UnidadeId { get; set; }
}
