using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
    /// <summary>
    /// Valida que el nombre no se repita al agregar
    /// </summary>
    public class AgregarNombreRegla : IRegla
    {
        private string nombre;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        /// <summary>
        /// Mensaje de error
        /// </summary>
        public CustomError customError { get; set; }

        /// <summary>
        /// Valida que una cateogría no se llame igual al agregar
        /// </summary>
        /// <param name="nombre">Nombre de la categoría</param>
        /// <param name="context">Objeto para la bd</param>
        /// <param name="locService">Objeto para mensajes en varios
        /// idiomas</param>
        public AgregarNombreRegla(string nombre, CaducaContext context, LocService locService)
        {
            this.nombre = nombre;
            this.contexto = context;
            this.localizacion = locService;
        }

        /// <summary>
        /// Permite validar que el nombre de una categoría no se
        /// repita al agregar
        /// </summary>
        /// <returns>True si no se repite la categoría</returns>
        public bool EsCorrecto()
        {
            var registroRepetido = contexto.Categoria.AsNoTracking().FirstOrDefault(c => c.Nombre == nombre);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "categoría", "nombre"), "Nombre");
                return false;
            }
            return true;
        }
    }
}