using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
    public class AgregarClaveRegla: IRegla
    {
        private int clave;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        public AgregarClaveRegla(int clave, CaducaContext context, LocService locService)
        {
            this.clave = clave;
            this.contexto = context;
            this.localizacion = locService;
        }
        public CustomError customError { get; set; }
       
        public bool ValidarRegla()
        {
            var registroRepetido = contexto.Categoria.FirstOrDefault(c => c.Clave == clave);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "categoría", "clave"), "Clave");
                return false;
            }
            return true;
        }
    }
}
