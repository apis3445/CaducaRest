using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para registrar los accesos de un usuario
    /// </summary>
    public class UsuarioAccesoConfiguration : IEntityTypeConfiguration<UsuarioAcceso>
    {
        /// <summary>
        /// LLaves foráneas e indices para registrar los accesos de un usuario
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UsuarioAcceso> builder)
        {
            builder.HasOne(typeof(Usuario))
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
                       
            builder.HasIndex(u => u.RefreshToken)
                .HasName("UI_RefreshToken")
                .IsUnique();

            builder.HasIndex(u => new { u.UsuarioId, u.Token })
                .HasName("UI_Token")
                .IsUnique();
        }
    }
}
