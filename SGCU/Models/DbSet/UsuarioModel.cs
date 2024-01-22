using System.ComponentModel.DataAnnotations;

namespace SGCU.Models.DbSet;

public class UsuarioModel
{
    [Key]
    public Guid UsuarioId { get; set; }

    public string Login { get; set; }
    public string Senha { get; set; }
    public bool Status { get; set; }
}
