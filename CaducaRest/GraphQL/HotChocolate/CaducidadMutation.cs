using System.Threading.Tasks;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using HotChocolate;

namespace CaducaRest.GraphQL.HotChocolate
{
    /// <summary>
    /// Funciones para agregar, borrar o modificar
    /// </summary>
    public class CaducidadMutation
    {
        public async Task<Caducidad> AddCaducidad([Service] CaducaContext caducaContext, [Service] LocService locService, Caducidad caducidad)
        {
            CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
            var correcto = await caducidadDAO.AgregarAsync(caducidad);
            if (correcto)
                return caducidad;
            else
                return new Caducidad();
        }

        public async Task<string> DeleteCaducidad([Service] CaducaContext caducaContext, [Service] LocService locService, int id)
        {
            CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
            var correcto = await caducidadDAO.BorraAsync(id);
            if (correcto)
                return $"La caducidad con el id: {id} fue borrada correctamente";
            else
                return caducidadDAO.customError.Message;
        }

        public async Task<Caducidad> UpdateCaducidad([Service] CaducaContext caducaContext,
                                                  [Service] LocService locService,
                                                  Caducidad caducidad,
                                                  int id)
        {
            if (id != caducidad.Id)
            {
                return null;
            }
            CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
            var correcto = await caducidadDAO.ModificarAsync(caducidad);
            if (correcto)
                return caducidad;
            else
                return null;
        }
    }
}
