using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.Linq;

namespace CaducaRest.Rules
{
    public class CategoriaNombreAgregarRegla : IRegla
    {
        private string nombre;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        public CategoriaNombreAgregarRegla(string nombre, CaducaContext context, LocService locService)
        {
            this.nombre = nombre;
            this.contexto = context;
            this.localizacion = locService;
        }
        public CustomError customError { get; set; }

        public bool ValidarRegla()
        {
            var registroRepetido = contexto.Categoria.FirstOrDefault(c => c.Nombre == nombre);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "categoría", "nombre"), "Nombre");
                return false;
            }
            return true;
        }
    }
}
