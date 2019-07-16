using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class TablaRolPermisoConfiguration : IEntityTypeConfiguration<RolTablaPermiso>
    {

        public void Configure(EntityTypeBuilder<RolTablaPermiso> builder)
        {
            builder.HasIndex(u => new { u.TablaPermisoId, u.RolId })
                .HasName("UI_TablaPermiso")
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
