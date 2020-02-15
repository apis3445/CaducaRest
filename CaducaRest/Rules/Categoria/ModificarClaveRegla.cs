using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
    /// <summary>
    /// Permite validar que no se repita una clave para
    /// una categoría
    /// </summary>
    public class ModificarClaveRegla : IRegla
    {
        private int clave;
        private int id;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        /// <summary>
        /// Mensaje de error
        /// </summary>
        public CustomError customError { get; set; }

        /// <summary>
        /// Permite validar que no se repita una clave de
        /// una categoría
        /// </summary>
        /// <param name="id">Id de la categoría</param>
        /// <param name="clave">Clave de la categoría</param>
        /// <param name="context">Objeto para ala bd</param>
        /// <param name="locService">Objeto para los mensajes de
        /// error en otros idiomas</param>
        public ModificarClaveRegla(int id, int clave, CaducaContext context, LocService locService)
        {
            this.clave = clave;
            this.contexto = context;
            this.localizacion = locService;
            this.id = id;
        }

        /// <summary>
        /// Permite validar que no se repita la clave de una categoría
        /// </summary>
        /// <returns></returns>
        public bool EsCorrecto()
        {
            var registroRepetido = contexto.Categoria.AsNoTracking().FirstOrDefault(c => c.Clave == clave
                                           && c.Id != id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "categoría", "clave"), "Clave");
                return false;
            }
            return true;
        }
    }
}
