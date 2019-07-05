using CaducaRest.Models.Entity_Configurations;
using CaducaRest.Models.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.ApplyConfiguration(new UsuarioCategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioClienteConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioRolConfiguration());

            #region Inserts
            modelBuilder.Entity<Rol>().HasData(
               new Rol { Id = 1,  Nombre = "Administrador" },
               new Rol { Id = 2,  Nombre = "Vendedor" },
               new Rol { Id = 3,  Nombre = "Cliente" }
           );
               modelBuilder.Entity<Usuario>().HasData(
                new Usuario

                {
                    Id = 1,
                    Activo = true,
                    Clave = "Juan",
                    Email = "correo@gmail.com",
                    Nombre = "Juan Peréz",
                    Adicional1 = "2a3efe03a96840478bde71ae36a20f2e",
                    Password = "9f9b901a43d795295661443f7f7098ee8e6c6c3694428717c54d5fd058220fed"
                },
                new Usuario
                {
                    Id = 2,
                    Activo = true,
                    Clave = "Maria",
                    Email = "correo@gmail.com",
                    Nombre = "Maria Lopez",
                    Adicional1 = "37b93bbd77b2d7a586cc7d5032f83808",
                    Password = "6ad9ebcfe2bebed6655a4abb3e0409c83ad1e6db35098083476744cfe0d106b9"
                },
                new Usuario

                {
                    Id = 3,
                    Activo = true,
                    Clave = "Carlos",
                    Email = "correo@gmail.com",
                    Nombre = "Carlos Hernández",
                    Adicional1 = "5dd69f799e8ac1fd877460c4d461eb74",
                    Password = "6c60e72d7ea36a7defc15f0b551cd739180d2254ddaf4c8833ece2ecf8b48c5a"
                }
                );

            modelBuilder.Entity<UsuarioRol>().HasData(
                new UsuarioRol
                {
                    Id = 1,
                    RolId = 3,
                    UsuarioId = 1
                },
                new UsuarioRol
                {
                    Id = 2,
                    RolId = 2,
                    UsuarioId = 2
                },
                new UsuarioRol
                {
                    Id = 3,
                    RolId = 1,
                    UsuarioId = 3
                }
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

        public virtual DbSet<UsuarioCategoria> UsuarioCategoria { get; set; }

        public virtual DbSet<UsuarioCliente> UsuarioCliente { get; set; }

        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
    }
}
