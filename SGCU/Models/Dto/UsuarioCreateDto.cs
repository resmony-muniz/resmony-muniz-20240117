namespace SGCU.Models.Dto;

public class UsuarioCreateDto
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; }
    public bool Status { get; set; }
}
