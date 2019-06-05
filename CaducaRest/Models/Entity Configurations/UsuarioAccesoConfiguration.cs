using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class UsuarioAccesoConfiguration : IEntityTypeConfiguration<UsuarioAcceso>
    {
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
