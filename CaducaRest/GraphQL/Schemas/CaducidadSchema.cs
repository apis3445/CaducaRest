using CaducaRest.GraphQL.Mutation;
using CaducaRest.GraphQL.Query;
using GraphQL;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Schemas
{
    /// <summary>
    /// Los objetos Schema contienen los métodos disponibles
    /// </summary>
    public class CaducidadSchema : Schema
    {
        /// <summary>
        /// Esquema para la caducidad
        /// Contiene métodos para consultar y modificar registros
        /// de caducidad de productos
        /// </summary>
        /// <param name="resolver"></param>
        public CaducidadSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CaducidadQuery>();

            Mutation = resolver.Resolve<CaducidadMutation>();
        }
    }
}
