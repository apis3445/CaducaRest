using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.Entity_Configurations
{
    /// <summary>
    /// LLaves e índices para guardar las categorías por cliente
    /// </summary>
    public class ClienteCategoriaConfiguration : IEntityTypeConfiguration<ClienteCategoria>
    {
        /// <summary>
        /// LLaves e índices para guardar las categorías por cliente
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ClienteCategoria> builder)
        {
            builder.HasOne(c => c.Categoria)
                    .WithMany(cc=>cc.ClientesCategorias)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Cliente)
                .WithMany(c => c.ClientesCategorias)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasIndex(e => new { e.ClienteId, e.CategoriaId })
               .HasName("UI_ClienteForo")
               .IsUnique();
        }
    }
}
