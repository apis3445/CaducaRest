using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaes foráneas e índices para las Caducidades
    /// </summary>
    public class CaducidadConfiguration : IEntityTypeConfiguration<Caducidad>
    {
        /// <summary>
        /// LLaves foráneas e índices para las Caducidades
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Caducidad> builder)
        {
            builder.HasOne(c => c.Cliente)
                     .WithMany(cc => cc.Caducidades)
                     .OnDelete(DeleteBehavior.Restrict);

             builder.HasOne(e => e.Producto)
               .WithMany(c => c.Caducidades)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasIndex(e => new { e.ClienteId, e.ProductoId, e.Fecha })
               .HasName("UI_ClienteProducto")
               .IsUnique();
        }
    }
}
