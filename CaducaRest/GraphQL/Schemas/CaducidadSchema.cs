using System;
using CaducaRest.GraphQL.Mutation;
using CaducaRest.GraphQL.Query;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

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
        /// <param name="provider"></param>
        public CaducidadSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<CaducidadQuery>();

            Mutation = provider.GetRequiredService<CaducidadMutation>();
        }
    }
}
