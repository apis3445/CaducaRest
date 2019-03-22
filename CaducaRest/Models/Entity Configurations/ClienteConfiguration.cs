using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.Entity_Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasIndex(e => e.Clave)
             .HasName("UI_ClienteCategoriaClave")
             .IsUnique();

            builder.HasIndex(e => e.RazonSocial)
            .HasName("UI_ClienteCategoriaNombre")
            .IsUnique();

           
        }
    }
}
