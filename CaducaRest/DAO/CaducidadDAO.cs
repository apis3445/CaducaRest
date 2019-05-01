using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
    public class CaducidadDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        private AccesoDAO<Caducidad> caducidadDAO;
        /// <summary>
        /// Mensaje de error personalizado
        /// </summary>
        public CustomError customError;

        /// <summary>
        /// Clase para acceso a la base de datos
        /// </summary>
        /// <param name="context">Objeto para base de datos</param>
        /// <param name="locService">Localización</param>
        public CaducidadDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
            caducidadDAO = new AccesoDAO<Caducidad>(context, locService);
        }

        public async Task<List<Caducidad>> ObtenerTodoAsync()
        {
            return await caducidadDAO.ObtenerTodoAsync();
        }
    }
}
