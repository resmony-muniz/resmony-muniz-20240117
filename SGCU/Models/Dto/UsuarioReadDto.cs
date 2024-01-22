namespace SGCU.Models.Dto;

public class UsuarioReadDto
{
    public Guid UsuarioId { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public bool? Status { get; set; }
}
