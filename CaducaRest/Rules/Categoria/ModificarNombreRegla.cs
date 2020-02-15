using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
    /// <summary>
    /// Regla para validar que el nombre de una categoría no se repita
    /// </summary>
    public class ModificarNombreRegla : IRegla
    {

        private string nombre;
        private int id;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        /// <summary>
        /// Mensaje de error
        /// </summary>
        public CustomError customError { get; set; }

        /// <summary>
        /// Permite validar que el nombre de la categoría no se repita
        /// para modificar
        /// </summary>
        /// <param name="id">Id del registro</param>
        /// <param name="nombre">Nombre de la categoría</param>
        /// <param name="context">Objeto para acceso a la bd</param>
        /// <param name="locService">Para traducir mensajes de error</param>
        public ModificarNombreRegla(int id, string nombre, CaducaContext context, LocService locService)
        {
            this.nombre = nombre;
            this.contexto = context;
            this.localizacion = locService;
            this.id = id;
        }

        /// <summary>
        /// Permite validar la regla de que la categoría no se puede repetir
        /// </summary>
        /// <returns></returns>
        public bool EsCorrecto()
        {
            var registroRepetido = contexto.Categoria.AsNoTracking().FirstOrDefault(c => c.Nombre == nombre
                                           && c.Id != id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "categoría", "nombre"), "Nombre");
                return false;
            }
            return true;
        }
    }
}
