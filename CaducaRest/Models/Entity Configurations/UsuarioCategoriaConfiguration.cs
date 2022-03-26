using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para las categorías a las
    /// que tiene acceso un usuario
    /// </summary>
    public class UsuarioCategoriaConfiguration : IEntityTypeConfiguration<UsuarioCategoria>
    {
        /// <summary>
        /// LLaves foráneas e indices para las categorías a las
        /// que tiene acceso un usuario
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UsuarioCategoria> builder)
        {
            builder.HasIndex(u => new { u.UsuarioId, u.CategoriaId })
                .HasDatabaseName("UI_UsuarioCategoria")
                .IsUnique();

            builder.HasOne(typeof(Usuario))
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(Categoria))
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
