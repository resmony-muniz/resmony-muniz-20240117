using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGCU.Models.DbSet;

public class ColaboradorModel
{
    [Key]
    public Guid ColaboradorId { get; set; }
    public string Nome { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public Guid UsuarioId { get; set; }

    [NotMapped]
    public UsuarioModel Usuario { get; set; }

    [ForeignKey("Unidade")]
    public Guid UnidadeId { get; set; }

    [NotMapped]
    public UnidadeModel Unidade { get; set; }
}
