using CaducaRest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.Entity_Configurations
{
    /// <summary>
    /// LLaves foráneas e indices
    /// </summary>
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        /// <summary>
        /// Llaves foráneas e indices
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasIndex(e => e.Clave )
             .HasName("UI_CategoriaClave")
             .IsUnique();

            builder.HasIndex(e => e.Nombre)
            .HasName("UI_CategoriaNombre")
            .IsUnique();
        }
    }
}
