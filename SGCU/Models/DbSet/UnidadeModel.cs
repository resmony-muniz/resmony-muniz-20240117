using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SGCU.Models.DbSet;

[Index(nameof(CodigoUnidade), IsUnique = true)]
public class UnidadeModel
{
    [Key]
    public Guid UnidadeId { get; set; }

    [StringLength(5), Required]
    public string CodigoUnidade { get; set; }
    public string Nome { get; set; }

    public bool Status { get; set; }
}
