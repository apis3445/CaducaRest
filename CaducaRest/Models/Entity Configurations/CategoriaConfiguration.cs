using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CaducaRest.Models.Entity_Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasIndex(e => e.Clave )
             .HasName("UI_CategoriaClave")
             .IsUnique();

            builder.HasIndex(e => e.Nombre)
            .HasName("UI_CategoriaNombre")
            .IsUnique();
        }
    }
}
