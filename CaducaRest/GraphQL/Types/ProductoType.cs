using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Types
{
    /// <summary>
    /// Clase para mapear los campos del producto
    /// a los manejados por GraphQL
    /// </summary>
    public class ProductoType : ObjectGraphType<Producto>
    {
        /// <summary>
        /// Constructor que permite mapear campos
        /// </summary>
        /// <param name="caducaContext"></param>
        /// <param name="locService">Objeto para mensajes en varios idiomas</param>
        public ProductoType(CaducaContext caducaContext, LocService locService)
        {
          
            Name = "Producto";

            Field(c => c.Id).Description("Id");
            Field(c => c.Clave).Description("Clave del producto");
            Field(c => c.Nombre).Description("Nombre del cliente");
            Field(c => c.CategoriaId).Description("Categoria");
           
        }
    }
}
