using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class UsuarioCategoriaConfiguration : IEntityTypeConfiguration<UsuarioCategoria>
    {
        public void Configure(EntityTypeBuilder<UsuarioCategoria> builder)
        {
            builder.HasIndex(u => new { u.UsuarioId, u.CategoriaId })
                .HasName("UI_UsuarioCategoria")
                .IsUnique();

            builder.HasOne(typeof(Usuario))
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(Categoria))
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
