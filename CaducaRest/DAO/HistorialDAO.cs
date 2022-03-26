using System;
using System.Linq;
using System.Threading.Tasks;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;

namespace CaducaRest.DAO
{
    /// <summary>
    /// Registra el historial de cambios
    /// </summary>
    public class HistorialDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="locService"></param>
        public HistorialDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
        }

        /// <summary>
        /// Permite agregar un nuevo historial
        /// </summary>
        /// <param name="usuarioId">Id del usuario</param>
        /// <param name="actividad">Tipo de actividad que realiza el usuario</param>
        /// <param name="nombreTabla">Tabla en la que se realiza el cambio</param>
        /// <param name="origenId">Id del registro modificado</param>
        /// <param name="observaciones">Observaciones de la modificación</param>
        /// <returns></returns>
        public async Task<bool> AgregarAsync(int usuarioId, int actividad,
                                             string nombreTabla, int origenId,
                                             string observaciones)
        {
            var tablaId = await ObtenerIdTablaAsync(nombreTabla);
            Historial historial = new Historial
            {
                Actividad = actividad,
                FechaHora = DateTime.Now,
                Observa = observaciones,
                OrigenId = origenId,
                TablaId = tablaId,
                UsuarioId = usuarioId
            };
            contexto.Historial.Add(historial);
            contexto.SaveChanges();
            return true;
        }
        /// <summary>
        /// Obtiene el id de una tabla
        /// </summary>
        /// <param name="nombreTabla">Nombre de la tabla de la que se desea obtener su id</param>
        /// <returns></returns>
        private async Task<int> ObtenerIdTablaAsync(string nombreTabla)
        {
            Tabla tabla = await contexto.Tabla.FirstOrDefaultAsync(e => e.Nombre == nombreTabla);
            if (tabla == null)
            {
                return -1;
            }
            return tabla.Id;
        }

        /// <summary>
        /// Borra el historial de cambios
        /// </summary>
        /// <param name="nombreTabla">Tabla de la que se desea borrar el historial</param>
        /// <param name="origenId">Id del registro del que se desea borrar el historial</param>
        /// <returns></returns>
        public async Task<bool> BorraAsync( string nombreTabla, int origenId)
        {
            var tablaId = await ObtenerIdTablaAsync(nombreTabla);
            var consulta = await (from historial in contexto.Historial
                            where historial.TablaId == tablaId
                            && historial.OrigenId == origenId                 
                            select historial).ToListAsync();
            contexto.Historial.RemoveRange(consulta);
            contexto.SaveChanges();
            return true;
        }
    }
}
