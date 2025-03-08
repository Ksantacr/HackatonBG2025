using bg.hackathon.alphahackers.domain.entities.pyme;
using Microsoft.EntityFrameworkCore;

namespace bg.hackathon.alphahackers.infrastructure.data.context
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Contactabilidad> Contactabilidades { get; set; }
        public DbSet<LineaCredito> LineasCredito { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones del modelo aquí
        }
    }
}
