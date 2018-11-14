using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
    /// <summary>
    /// Funciones para acceso a los datos para las categorías
    /// </summary>
    public class CategoriaDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService _localizer;
        /// <summary>
        /// Mensaje de error personalizado
        /// </summary>
        public CustomError customError;

        /// <summary>
        /// Clase para acceso a la base de datos
        /// </summary>
        /// <param name="context"></param>
        public CategoriaDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this._localizer = locService;
        }

        /// <summary>
        /// Obtiene todas las categorias
        /// </summary>
        /// <returns></returns>
        public List<Categoria> ObtenerTodo()
        {
            return contexto.Categoria.ToList();
        }

        /// <summary>
        /// Obtienen una categoría por us Id
        /// </summary>
        /// <param name="id">Id de la categoría</param>
        /// <returns></returns>
        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            return await contexto.Categoria.FindAsync(id);
        }

        /// <summary>
        /// Permite agregar una nueva categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public async Task<bool> AgregarAsync(Categoria categoria)
        {
            Categoria registroRepetido;
            try
            {
                registroRepetido = contexto.Categoria.FirstOrDefault(c => c.Nombre == categoria.Nombre);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400, String.Format(this._localizer.GetLocalizedHtmlString("Repeteaded"), "categoría", "nombre"), "Nombre");
                    return false;
                }
                registroRepetido = contexto.Categoria.FirstOrDefault(c => c.Clave == categoria.Clave);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                                            "Ya existe una categoría con esta clave, " +
                                            "por favor teclea una clave diferente", "Nombre");
                    return false;
                }

                contexto.Categoria.Add(categoria);
                await contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Modidica una categoria
        /// </summary>
        /// <param name="categoria">Datos de la categoria</param>
        /// <returns></returns>
        public async Task<bool> ModificarAsync(Categoria categoria)
        {
            Categoria registroRepetido;
            try
            {
                //Se busca si existe una categoria con el mismo nombre pero diferente Id
                registroRepetido = contexto.Categoria.FirstOrDefault(c => c.Nombre == categoria.Nombre
                                                && c.Id != categoria.Id);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                                            "Ya existe una categoría con este nombre, " +
                                            "por favor teclea un nombre diferente", "Nombre");
                    return false;
                }
                registroRepetido = contexto.Categoria.FirstOrDefault(c => c.Clave == categoria.Clave
                                                && c.Id != categoria.Id);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                                            "Ya existe una categoría con esta clave, " +
                                            "por favor teclea una clave diferente", "Nombre");
                    return false;
                }
                contexto.Entry(categoria).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteCategoria(categoria.Id))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Permite borrar una categoría por Id
        /// </summary>
        /// <param name="id">Id de la categoría</param>
        /// <returns></returns>
        public async Task<bool> BorraAsync(int id)
        {
            var categoria = await ObtenerPorIdAsync(id);
            if (categoria == null)
            {
                customError = new CustomError(404,
                                            "La categoría que deseas borrar ya no existe, " +
                                            "probablemente fue borrada por otro usuario", "Id");
                return false;
            }

            contexto.Categoria.Remove(categoria);
            await contexto.SaveChangesAsync();
            return true;
        }

        private bool ExisteCategoria(int id)
        {
            return contexto.Categoria.Any(e => e.Id == id);
        }
    }
}