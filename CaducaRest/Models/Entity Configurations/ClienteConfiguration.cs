using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.Entity_Configurations
{
    /// <summary>
    /// LLaves foráneas e indices para los clientes
    /// </summary>
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        /// <summary>
        /// LLaves foráneas e indices para los clientes
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasIndex(e => e.Clave)
             .HasDatabaseName("UI_ClienteCategoriaClave")
             .IsUnique();

            builder.HasIndex(e => e.RazonSocial)
            .HasDatabaseName("UI_ClienteCategoriaNombre")
            .IsUnique();

           
        }
    }
}
