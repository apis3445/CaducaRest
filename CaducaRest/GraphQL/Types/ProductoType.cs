using System;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Types
{
    public class ProductoType : ObjectGraphType<Producto>
    {
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
