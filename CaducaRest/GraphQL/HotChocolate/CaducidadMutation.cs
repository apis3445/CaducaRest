using System.Threading.Tasks;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using HotChocolate;

namespace CaducaRest.GraphQL.HotChocolate;

/// <summary>
/// Funciones para agregar, borrar o modificar
/// </summary>
public class CaducidadMutation
{
    /// <summary>
    /// Add caducidad with hotchocolate
    /// </summary>
    /// <param name="caducaContext">Caduca Context</param>
    /// <param name="locService">Servicio para localization</param>
    /// <param name="caducidad">Objeto caducidad a agregar</param>
    /// <returns></returns>
    public async Task<Caducidad> AddCaducidad([Service] CaducaContext caducaContext, [Service] LocService locService, Caducidad caducidad)
    {
        Caducidad nuevo = new Caducidad();
        nuevo.Cantidad = caducidad.Cantidad;
        nuevo.ClienteId = caducidad.ClienteId;
        nuevo.ProductoId = caducidad.ProductoId;
        nuevo.Fecha = caducidad.Fecha;
        CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
        var correcto = await caducidadDAO.AgregarAsync(nuevo);
        if (correcto)
            return nuevo;
        else
            return new Caducidad();
    }

    /// <summary>
    /// Delete caducidsad
    /// </summary>
    /// <param name="caducaContext">Caduca Contexto</param>
    /// <param name="locService">Servicio para localization</param>
    /// <param name="id">Id a borrar</param>
    /// <returns></returns>
    public async Task<string> DeleteCaducidad([Service] CaducaContext caducaContext, [Service] LocService locService, int id)
    {
        CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
        var correcto = await caducidadDAO.BorraAsync(id);
        if (correcto)
            return $"La caducidad con el id: {id} fue borrada correctamente";
        else
            return caducidadDAO.customError.Message;
    }

    /// <summary>
    /// Update caducidad
    /// </summary>
    /// <param name="caducaContext">Caduca Contexto</param>
    /// <param name="locService">Servicio para localization</param>
    /// <param name="caducidad">Datos de caducidad a modificar</param>
    /// <param name="id">Id a actualizar</param>
    /// <returns></returns>
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
