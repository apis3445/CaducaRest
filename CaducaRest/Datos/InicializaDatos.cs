using System;
using CaducaRest.Models;

namespace CaducaRest.Datos
{
    /// <summary>
    /// Permite inicializar datos de prueba
    /// </summary>
    public class InicializaDatos
    {
        /// <summary>
        /// Constructro de la clase
        /// </summary>
        /// <param name="contexto"></param>
        public static void Inicializar(CaducaContext contexto)
        {
            //Si no es base de datos en memoria no se agrega nada
            if (contexto.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                return;
            //Te aseguras que la base de datos haya sido creada
            contexto.Database.EnsureCreated();

            var categorias = new Categoria[]
            {
                /*01*/ new Categoria { Clave = 1, Nombre = "Análgesicos"},
            };
            foreach (Categoria registro in categorias)
            {
                contexto.Categoria.Add(registro);
            }

            var productos = new Producto[]
            {
                /*01*/ new Producto { Clave = 1, Nombre = "Producto 1"},
            };
            foreach (Producto registro in productos)
            {
                contexto.Producto.Add(registro);
            }

            var clientes = new Cliente[]
            {
                /*01*/ new Cliente { Clave = 1, NombreComercial="Cliente 1", Activo = true, RazonSocial="Cliente 1", Direccion= "Calle #1"},
            };
            foreach (Cliente registro in clientes)
            {
                contexto.Cliente.Add(registro);
            }


            var caducidades = new Caducidad[]
            {
                /*01*/ new Caducidad { ClienteId=1, ProductoId=1, Cantidad=5, Fecha=DateTime.Now},
            };
            foreach (Caducidad registro in caducidades)
            {
                contexto.Caducidad.Add(registro);
            }


            contexto.SaveChanges();
        }
    }
}
