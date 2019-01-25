using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
    public class ModificarClaveRegla : IRegla
    {
        private int clave;
        private int id;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        public CustomError customError { get; set; }

        public ModificarClaveRegla(int id, int clave, CaducaContext context, LocService locService)
        {
            this.clave = clave;
            this.contexto = context;
            this.localizacion = locService;
            this.id = id;
        }
        
        public bool ValidarRegla()
        {
            var registroRepetido = contexto.Categoria.FirstOrDefault(c => c.Clave == clave
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
