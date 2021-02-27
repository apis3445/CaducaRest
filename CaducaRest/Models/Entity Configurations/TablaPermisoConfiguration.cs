using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para la configuración de permisos por tabla
    /// </summary>
    public class TablaPermisoConfiguration : IEntityTypeConfiguration<TablaPermiso>
    {
        /// <summary>
        /// LLaves foráneas e indices para la configuración de permisos por tabla
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<TablaPermiso> builder)
        {
            builder.HasIndex(u => new { u.TablaId, u.PermisoId })
                .HasDatabaseName("UI_TablaPermiso")
                .IsUnique();

            builder.HasOne(typeof(Tabla))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(Permiso))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
