using System;
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
            Database.EnsureCreated();
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
            try
            {
                modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
                modelBuilder.ApplyConfiguration(new ProductoConfiguration());
                modelBuilder.ApplyConfiguration(new ClienteConfiguration());
                modelBuilder.ApplyConfiguration(new CaducidadConfiguration());
                modelBuilder.ApplyConfiguration(new ClienteCategoriaConfiguration());

                modelBuilder.ApplyConfiguration(new HistorialConfiguration());
                modelBuilder.ApplyConfiguration(new TablaPermisoConfiguration());
                modelBuilder.ApplyConfiguration(new TablaRolPermisoConfiguration());

                modelBuilder.ApplyConfiguration(new UsuarioAccesoConfiguration());
                modelBuilder.ApplyConfiguration(new UsuarioCategoriaConfiguration());
                modelBuilder.ApplyConfiguration(new UsuarioClienteConfiguration());
                modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
                modelBuilder.ApplyConfiguration(new UsuarioRolConfiguration());

                #region Inserts
                modelBuilder.Entity<Rol>().HasData(
                   new Rol { Id = 1, Nombre = "Administrador" },
                   new Rol { Id = 2, Nombre = "Vendedor" },
                   new Rol { Id = 3, Nombre = "Cliente" },
                   new Rol { Id = 4, Nombre = "Supervisor" }
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
                    Email = "carlos@gmail.com",
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
                });



                modelBuilder.Entity<Permiso>().HasData(
                new Permiso
                {
                    Id = 1,
                    Clave = 1,
                    Nombre = "Crear",
                },
                new Permiso
                {
                    Id = 2,
                    Clave = 2,
                    Nombre = "Modificar",
                },
                new Permiso
                {
                    Id = 3,
                    Clave = 3,
                    Nombre = "Borrar",
                },
                new Permiso
                {
                    Id = 4,
                    Clave = 4,
                    Nombre = "Consultar",
                }
                );

                modelBuilder.Entity<Tabla>().HasData(
                new Tabla
                {
                    Id = 1,
                    Nombre = "Caducidad",
                    Descripción = "Permite registrar las fechas de caducidad de los productos"
                },
                new Tabla
                {
                    Id = 2,
                    Nombre = "Categoria",
                    Descripción = "Permite registrar las categorias de los productos"
                },
                new Tabla
                {
                    Id = 3,
                    Nombre = "Cliente",
                    Descripción = "Permite registrar los clientes"
                },
                new Tabla
                {
                    Id = 4,
                    Nombre = "ClienteCategoria",
                    Descripción = "Permite registrar las categorías de productos de cada cliente"
                },
                new Tabla
                {
                    Id = 5,
                    Nombre = "Producto",
                    Descripción = "Permite registrar los productos"
                },
                new Tabla
                {
                    Id = 6,
                    Nombre = "Permiso",
                    Descripción = "Permite registrar los permisos para el sistema"
                },
                new Tabla
                {
                    Id = 7,
                    Nombre = "Producto",
                    Descripción = "Permite registrar los productos"
                },
                new Tabla
                {
                    Id = 8,
                    Nombre = "Rol",
                    Descripción = "Permite registrar los roles de los usuarios"
                },
                new Tabla
                {
                    Id = 9,
                    Nombre = "Usuario",
                    Descripción = "Permite registrar los usuarios del sistema"
                },
                new Tabla
                {
                    Id = 10,
                    Nombre = "UsuarioCategoria",
                    Descripción = "Permite registrar las categorias de los usuarios del sistema"
                },
                new Tabla
                {
                    Id = 11,
                    Nombre = "UsuarioCliente",
                    Descripción = "Permite registrar los clientes para los usuarios del sistema"
                },
                new Tabla
                {
                    Id = 12,
                    Nombre = "UsuarioRol",
                    Descripción = "Permite registrar los roles para los usuarios del sistema"
                }

                );

                modelBuilder.Entity<TablaPermiso>().HasData(
                    new TablaPermiso
                    {
                        Id = 1,
                        PermisoId = 1,
                        TablaId = 2
                    },
                    new TablaPermiso
                    {
                        Id = 2,
                        PermisoId = 2,
                        TablaId = 2
                    },
                    new TablaPermiso
                    {
                        Id = 3,
                        PermisoId = 3,
                        TablaId = 2
                    },
                    new TablaPermiso
                    {
                        Id = 4,
                        PermisoId = 4,
                        TablaId = 2
                    },
                    new TablaPermiso
                    {
                        Id = 5,
                        PermisoId = 1,
                        TablaId = 3
                    },
                    new TablaPermiso
                    {
                        Id = 6,
                        PermisoId = 2,
                        TablaId = 3
                    },
                    new TablaPermiso
                    {
                        Id = 7,
                        PermisoId = 3,
                        TablaId = 3
                    },

                    new TablaPermiso
                    {
                        Id = 8,
                        PermisoId = 4,
                        TablaId = 3
                    },
                    new TablaPermiso
                    {
                        Id = 9,
                        PermisoId = 1,
                        TablaId = 5
                    },
                    new TablaPermiso
                    {
                        Id = 10,
                        PermisoId = 2,
                        TablaId = 5
                    },
                   new TablaPermiso
                   {
                       Id = 11,
                       PermisoId = 3,
                       TablaId = 5
                   },
                    new TablaPermiso
                    {
                        Id = 12,
                        PermisoId = 4,
                        TablaId = 5
                    }
               );

                modelBuilder.Entity<RolTablaPermiso>().HasData(
                     new RolTablaPermiso
                     {
                         Id = 1,
                         TablaPermisoId = 1,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 2,
                         TablaPermisoId = 2,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 3,
                         TablaPermisoId = 3,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 4,
                         TablaPermisoId = 4,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 5,
                         TablaPermisoId = 4,
                         RolId = 2,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 6,
                         TablaPermisoId = 4,
                         RolId = 3,
                         TienePermiso = true
                     },


                     new RolTablaPermiso
                     {
                         Id = 7,
                         TablaPermisoId = 5,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 8,
                         TablaPermisoId = 6,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 9,
                         TablaPermisoId = 7,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 10,
                         TablaPermisoId = 8,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 11,
                         TablaPermisoId = 8,
                         RolId = 2,
                         TienePermiso = true
                     },


                     new RolTablaPermiso
                     {
                         Id = 12,
                         TablaPermisoId = 9,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 13,
                         TablaPermisoId = 10,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 14,
                         TablaPermisoId = 11,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 15,
                         TablaPermisoId = 12,
                         RolId = 4,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 16,
                         TablaPermisoId = 12,
                         RolId = 2,
                         TienePermiso = true
                     },
                     new RolTablaPermiso
                     {
                         Id = 17,
                         TablaPermisoId = 12,
                         RolId = 3,
                         TienePermiso = true
                     }
                   );

                #endregion Inserts
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
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

        public virtual DbSet<RolTablaPermiso> RolTablaPermiso { get; set; }

        /// <summary>
        /// Categorías que maneja cada cliente
        /// </summary>
        public virtual DbSet<ClienteCategoria> ClienteCategoria { get; set; }


        public virtual DbSet<Historial> Historial { get; set; }

        public virtual DbSet<Permiso> Permiso { get; set; }

        public virtual DbSet<Tabla> Tabla { get; set; }

        public virtual DbSet<TablaPermiso> TablaPermiso { get; set; }

     
        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<UsuarioAcceso> UsuarioAcceso { get; set; }

        public virtual DbSet<UsuarioCategoria> UsuarioCategoria { get; set; }

        public virtual DbSet<UsuarioCliente> UsuarioCliente { get; set; }       

        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
    }
}
