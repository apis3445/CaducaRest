using CaducaRest.DAO;
using CaducaRest.GraphQL.Types;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace CaducaRest.GraphQL.Query
{
    /// <summary>
    /// Los objetos query en GraphQL permiten consultar información
    /// </summary>
    public class CaducidadQuery: ObjectGraphType
    {
        /// <summary>
        /// Constructor de la clase Query para consultar las caducidades
        /// </summary>
        /// <param name="caducaContext">Objeto para el acceso a la bd</param>
        /// <param name="locService">Objeto para mensajes de error en varios idiomas</param>
        public CaducidadQuery( LocService locService)
        {
            
            Field<ListGraphType<CaducidadType>>(
                "caducidades",
                resolve:  context =>
                    {
                        using var scope = context.RequestServices.CreateScope();
                        var services = scope.ServiceProvider;
                        var caducaContext = services.GetRequiredService<CaducaContext>();
                        CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
                        return caducidadDAO.ObtenerTodoAsync();
                        }
                );
        }
    }
}
