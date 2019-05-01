using CaducaRest.Models.Entity_Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CaducaRest.Models
{
    /// <summary>
    /// Contexto de la base de datos
    /// </summary>
    public class CaducaContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public CaducaContext(DbContextOptions<CaducaContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// Creación del modelo
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Si deseas restringir el borrado de todas tus tablas
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

           

            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new ProductoConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteCategoriaConfiguration());
        }

        /// <summary>
        /// Categorías para los productos
        /// </summary>
        public virtual DbSet<Caducidad> Categoria { get; set; }

        /// <summary>
        /// Productos
        /// </summary>
        public virtual DbSet<Producto> Producto { get; set; }

        /// <summary>
        /// Clientes
        /// </summary>
        public virtual DbSet<Cliente> Cliente { get; set; }

        /// <summary>
        /// Categorías que maneja cada cliente
        /// </summary>
        public virtual DbSet<ClienteCategoria> ClienteCategoria { get; set; }
    }
}
