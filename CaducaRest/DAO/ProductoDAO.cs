﻿using CaducaRest.Core;
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
            Producto registroRepetido;
            registroRepetido = contexto.Producto.FirstOrDefault(c => c.Nombre == Producto.Nombre);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "nombre"), "Nombre");
                return false;
            }
            registroRepetido = contexto.Producto.FirstOrDefault(c => c.Clave == Producto.Clave);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "clave"), "Clave");
                return false;
            }

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
            Producto registroRepetido;

            //Se busca si existe una Producto con el mismo nombre pero diferente Id
            registroRepetido = contexto.Producto.FirstOrDefault(c => c.Nombre == Producto.Nombre
                                            && c.Id != Producto.Id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "nombre"), "Nombre");
                return false;
            }
            registroRepetido = contexto.Producto.FirstOrDefault(c => c.Clave == Producto.Clave
                                            && c.Id != Producto.Id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "clave"), "Clave");
                return false;
            }
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

    }
}
