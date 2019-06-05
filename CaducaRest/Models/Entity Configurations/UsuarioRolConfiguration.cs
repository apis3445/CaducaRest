using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
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
