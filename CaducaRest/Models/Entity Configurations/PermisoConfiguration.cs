using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    /// <summary>
    /// LLaves e indices para Permiso Configuracion
    /// </summary>
    public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
    {
        /// <summary>
        /// Confgurar las llaves e indices
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.HasIndex(e => e.Clave)
                .IsUnique()
                .HasDatabaseName("UI_PermisoClave");
        }
    }
}