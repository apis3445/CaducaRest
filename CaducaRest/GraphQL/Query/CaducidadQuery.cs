using CaducaRest.DAO;
using CaducaRest.GraphQL.Types;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;

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
        public CaducidadQuery(CaducaContext caducaContext, LocService locService)
        {
            CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);

            Field<ListGraphType<CaducidadType>>(
                "caducidades",
                resolve: context => caducidadDAO.ObtenerTodoAsync()
                );
        }
    }
}
