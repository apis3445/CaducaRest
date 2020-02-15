using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para los permisos que tiene un rol en
    /// una tabla
    /// </summary>
    public class TablaRolPermisoConfiguration : IEntityTypeConfiguration<RolTablaPermiso>
    {
        /// <summary>
        /// LLaves foráneas e indices para los permisos que tiene un rol en
        /// una tabla
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<RolTablaPermiso> builder)
        {
            builder.HasIndex(u => new { u.TablaPermisoId, u.RolId })
                .HasName("UI_RolTablaPermiso")
                .IsUnique();

            builder.HasOne(typeof(TablaPermiso))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(Rol))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
