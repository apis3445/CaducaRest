using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para los usuarios
    /// </summary>
    public class UsuarioConfiguration: IEntityTypeConfiguration<Usuario>
    {
        /// <summary>
        /// LLaves foráneas e indices para los usuarios
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasIndex(e => e.Clave)
             .HasDatabaseName("UI_UsuarioClave")
             .IsUnique();
        }
    }
}