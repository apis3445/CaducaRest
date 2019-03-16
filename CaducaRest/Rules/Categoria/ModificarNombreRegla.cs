using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria
{
    public class ModificarNombreRegla : IRegla
    {

        private string nombre;
        private int id;
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        public CustomError customError { get; set; }

        public ModificarNombreRegla(int id, string nombre, CaducaContext context, LocService locService)
        {
            this.nombre = nombre;
            this.contexto = context;
            this.localizacion = locService;
            this.id = id;
        }

        public bool ValidarRegla()
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
