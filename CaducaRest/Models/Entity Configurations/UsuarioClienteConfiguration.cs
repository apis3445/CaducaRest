using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para los clientes a los que tiene
    /// acceso un usuario
    /// </summary>
    public class UsuarioClienteConfiguration : IEntityTypeConfiguration<UsuarioCliente>
    {
        /// <summary>
        /// LLaves foráneas e indices para los clientes a los que tiene
        /// acceso un usuario
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UsuarioCliente> builder)
        {
            builder.HasIndex(u => new { u.UsuarioId, u.ClienteId })
               .HasDatabaseName("UI_UsuarioCliente")
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