using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Rules
{
    public class CategoriaClaveAgregarRegla: IRegla
    {
        private int clave;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        public CategoriaClaveAgregarRegla(int clave, CaducaContext context, LocService locService)
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
