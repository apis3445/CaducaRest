using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class UsuarioClienteConfiguration : IEntityTypeConfiguration<UsuarioCliente>
    {
        public void Configure(EntityTypeBuilder<UsuarioCliente> builder)
        {
            builder.HasIndex(u => new { u.UsuarioId, u.ClienteId })
               .HasName("UI_UsuarioCliente")
               .IsUnique();

            builder.HasOne(typeof(Usuario))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(Cliente))
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
