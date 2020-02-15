using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para los roles de los usuarios
    /// </summary>
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
        /// <summary>
        /// LLaves foráneas e indices para los roles de los usuarios
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            builder.HasIndex(u => new { u.UsuarioId, u.RolId })
               .HasName("UI_UsuarioRol")
               .IsUnique();

            builder.HasOne(typeof(Usuario))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(Rol))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
