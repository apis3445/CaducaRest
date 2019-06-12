using CaducaRest.Models.Entity_Configurations;
using CaducaRest.Models.EntityConfigurations;
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
            modelBuilder.ApplyConfiguration(new CaducidadConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteCategoriaConfiguration());

            modelBuilder.ApplyConfiguration(new HistorialConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioAccesoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioClienteConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioRolConfiguration());

            #region Inserts
            modelBuilder.Entity<Rol>().HasData(
               new Rol { Id = 1,  Nombre = "Administrador" },
               new Rol { Id = 2,  Nombre = "Vendedor" },
               new Rol { Id = 3,  Nombre = "Cliente" }
           );

            #endregion Inserts
        }

        /// <summary>
        /// Categorías para los productos
        /// </summary>
        public virtual DbSet<Categoria> Categoria { get; set; }

        /// <summary>
        /// Productos
        /// </summary>
        public virtual DbSet<Producto> Producto { get; set; }

        /// <summary>
        /// Clientes
        /// </summary>
        public virtual DbSet<Cliente> Cliente { get; set; }

        /// <summary>
        /// Caducidad
        /// </summary>
        /// <value>The caducidad.</value>
        public virtual DbSet<Caducidad> Caducidad { get; set; }

        /// <summary>
        /// Crea la tabla rol, la cual nos permite clasificar a los usuarios
        /// en los siguientes tipos:
        /// Administrador
        /// Vendedor
        /// Cliente
        /// </summary>
        /// <value>Rol</value>
        public virtual DbSet<Rol>  Rol { get; set; }

        /// <summary>
        /// Categorías que maneja cada cliente
        /// </summary>
        public virtual DbSet<ClienteCategoria> ClienteCategoria { get; set; }

        public virtual DbSet<Historial> Historial { get; set; }

        public virtual DbSet<Tabla> Tabla { get; set; }

        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<UsuarioAcceso> UsuarioAcceso { get; set; }

        public virtual DbSet<UsuarioCliente> UsuarioCliente { get; set; }

        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
    }
}
