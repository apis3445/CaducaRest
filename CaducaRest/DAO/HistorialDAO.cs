using System;
using System.Linq;
using System.Threading.Tasks;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;

namespace CaducaRest.DAO
{
    public class HistorialDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        public HistorialDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
        }

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

        private async Task<int> ObtenerIdTablaAsync(string nombreTabla)
        {
            Tabla tabla = await contexto.Tabla.FirstOrDefaultAsync(e => e.Nombre == nombreTabla);
            if (tabla == null)
            {
                return -1;
            }
            return tabla.Id;
        }

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
