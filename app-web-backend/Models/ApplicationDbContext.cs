using Microsoft.EntityFrameworkCore;

namespace app_web_backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbSet<Usuario>? usuarios)
        {
            Usuarios = usuarios;
        }

        public DbSet<Veiculos>? Veiculos { get; set; }
        public DbSet<Consumo>?  Consumos { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
    }
}
