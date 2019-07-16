using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class TablaPermisoConfiguration : IEntityTypeConfiguration<TablaPermiso>
    {
        public void Configure(EntityTypeBuilder<TablaPermiso> builder)
        {
            builder.HasIndex(u => new { u.TablaId, u.PermisoId })
                .HasName("UI_TablaPermiso")
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
