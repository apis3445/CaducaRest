using CaducaRest.Models.Entity_Configurations;
using Microsoft.EntityFrameworkCore;

namespace CaducaRest.Models
{
    public class CaducaContext : DbContext
    {
        public CaducaContext(DbContextOptions<CaducaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        }
        public virtual DbSet<Categoria> Categoria { get; set; }
    }
}
