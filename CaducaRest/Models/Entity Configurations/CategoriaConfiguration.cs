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
             .HasDatabaseName("UI_CategoriaClave")
             .IsUnique();

            builder.HasIndex(e => e.Nombre)
            .HasDatabaseName("UI_CategoriaNombre")
            .IsUnique();
        }
    }
}
