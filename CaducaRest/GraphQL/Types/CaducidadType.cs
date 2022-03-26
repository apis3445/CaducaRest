using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace CaducaRest.GraphQL.Types
{
    /// <summary>
    /// Caducidad type.
    /// </summary>
    public class CaducidadType : ObjectGraphType<Caducidad>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CaducaRest.GraphQL.Types.CaducidadType"/> class.
        /// </summary>
        /// <param name="locService">Location service.</param>
        public CaducidadType(LocService locService)
        {

            Name = "Caducidad";

            Field(c => c.Id).Description("Id");
            Field(c => c.ProductoId).Description("Id del producto");
            Field(c => c.ClienteId).Description("Id del cliente");
            Field(c => c.Cantidad).Description("Cantidad");
            Field(c => c.Fecha).Description("Fecha");

            Field<ProductoType>("Producto",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "Id" }),
                resolve: context =>
                {
                   using var scope = context.RequestServices.CreateScope();
                   var services = scope.ServiceProvider;
                   var caducaContext = services.GetRequiredService<CaducaContext>();
                   ProductoDAO productoDAO = new ProductoDAO(caducaContext, locService);
                   return productoDAO.ObtenerPorIdAsync(context.Source.Id).Result;
                });

            Field<ClienteType>("Cliente", 
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "Id" }),
                resolve: context => {
                    using var scope = context.RequestServices.CreateScope();
                    var services = scope.ServiceProvider;
                    var caducaContext = services.GetRequiredService<CaducaContext>();
                    ClienteDAO clienteDAO = new ClienteDAO(caducaContext, locService);
                    return clienteDAO.ObtenerPorIdAsync(context.Source.Id).Result;
                });
            
        }
    }
}
