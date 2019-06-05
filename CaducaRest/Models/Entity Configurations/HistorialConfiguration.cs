using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class HistorialConfiguration : IEntityTypeConfiguration<Historial>
    {
        public void Configure(EntityTypeBuilder<Historial> builder)
        {
            builder.HasIndex(h => h.TabladId)
                   .HasName("IX_HistorialTabla");

            builder.HasIndex(e => e.UsuarioId)
              .HasName("IX_ctrUsuario");

            builder.HasIndex(e => new { e.Actividad, e.TabladId, e.FechaHora })
              .HasName("IX_Actividad");

            builder.HasIndex(e => new { e.TabladId, e.OrigenId, e.Actividad })
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
