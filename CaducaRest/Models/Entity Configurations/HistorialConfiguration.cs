using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves foráneas e indices para el historial de cambios
    /// </summary>
    public class HistorialConfiguration : IEntityTypeConfiguration<Historial>
    {
        /// <summary>
        /// LLaves foráneas e indices para el historial de cambios
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Historial> builder)
        {
            builder.HasIndex(h => h.TablaId)
                   .HasName("IX_HistorialTabla");

            builder.HasIndex(e => e.UsuarioId)
              .HasName("IX_ctrUsuario");

            builder.HasIndex(e => new { e.Actividad, e.TablaId, e.FechaHora })
              .HasName("IX_Actividad");

            builder.HasIndex(e => new { e.TablaId, e.OrigenId, e.Actividad })
              .HasName("IX_Historial");

            builder.HasOne(typeof(Tabla))
              .WithMany()
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(typeof(Usuario))
              .WithMany()
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}