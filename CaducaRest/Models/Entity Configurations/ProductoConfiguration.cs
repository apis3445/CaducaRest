using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.Entity_Configurations
{
    /// <summary>
    /// Llaves foráneas e índices
    /// </summary>
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        /// <summary>
        /// Llaves foráneas e índices
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasIndex(e => e.CategoriaId)
             .HasDatabaseName("IX_ProductoCategoria");

            builder.HasOne(typeof(Caducidad))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.Nombre)
                .IsUnique()
                .HasDatabaseName("UI_ProductoNombre");

            builder.HasIndex(e => e.Clave)
                .IsUnique()
                .HasDatabaseName("UI_ProductoClave");

        }
    }
}
