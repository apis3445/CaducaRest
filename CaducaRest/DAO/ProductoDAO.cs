using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
    /// <summary>
    /// Funciones de acceso a datos para los Productos
    /// </summary>
    public class ProductoDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        /// <summary>
        /// Mensaje de error personalizado
        /// </summary>
        public CustomError customError;

        /// <summary>
        /// Clase para acceso a la base de datos
        /// </summary>
        /// <param name="context"></param>
        /// <param name="locService"></param>
        public ProductoDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
        }

        /// <summary>
        /// Obtiene todas las Productos
        /// </summary>
        /// <returns></returns>
        public List<Producto> ObtenerTodo()
        {
            return contexto.Producto.ToList();
        }

        /// <summary>
        /// Obtienen una Producto por us Id
        /// </summary>
        /// <param name="id">Id de la Producto</param>
        /// <returns></returns>
        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            return await contexto.Producto.FindAsync(id);
        }

        /// <summary>
        /// Permite agregar una nueva Producto
        /// </summary>
        /// <param name="Producto"></param>
        /// <returns></returns>
        public async Task<bool> AgregarAsync(Producto Producto)
        {
            contexto.Producto.Add(Producto);
            await contexto.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Modidica una Producto
        /// </summary>
        /// <param name="Producto">Datos de la Producto</param>
        /// <returns></returns>
        public async Task<bool> ModificarAsync(Producto Producto)
        {
            contexto.Entry(Producto).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Permite borrar una Producto por Id
        /// </summary>
        /// <param name="id">Id de la Producto</param>
        /// <returns></returns>
        public async Task<bool> BorraAsync(int id)
        {
            var Producto = await ObtenerPorIdAsync(id);
            if (Producto == null)
            {
                customError = new CustomError(404, String.Format(this.localizacion.GetLocalizedHtmlString("NotFound"), "La Producto"), "Id");
                return false;
            }
            contexto.Producto.Remove(Producto);
            await contexto.SaveChangesAsync();
            return true;
        }

        private bool ExisteProducto(int id)
        {
            return contexto.Producto.Any(e => e.Id == id);
        }

        /// <summary>
        /// Permite validar si el nombre del producto se repite
        /// </summary>
        /// <param name="id">Id del producto</param>
        /// <param name="nombre">Nombre del producto</param>
        /// <returns></returns>
        public bool EsNombreRepetido(int id, string nombre)
        {
            var registroRepetido = contexto.Producto.FirstOrDefault(c => c.Nombre == nombre
                                          && c.Id != id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "nombre"), "Nombre");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permite validar si la clave del producto se repite
        /// </summary>
        /// <param name="id">Id del producto</param>
        /// <param name="clave">Clave del producto</param>
        /// <returns></returns>
        public bool EsClaveRepetida(int id, int clave)
        {
            var registroRepetido = contexto.Producto.FirstOrDefault(c => c.Clave == clave
                                          && c.Id != id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "clave"), "Clave");
                return true;
            }
            return false;
        }
    }
}
