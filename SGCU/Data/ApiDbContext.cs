using Microsoft.EntityFrameworkCore;
using SGCU.Models.DbSet;

namespace SGCU.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    { }


    public DbSet<UsuarioModel> Usuarios { get; set; }
    public DbSet<ColaboradorModel> Colaboradores { get; set; }
    public DbSet<UnidadeModel> Unidades { get; set; }
}
