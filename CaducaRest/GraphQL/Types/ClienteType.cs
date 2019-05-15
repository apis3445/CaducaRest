using System;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Types
{
    public class ClienteType : ObjectGraphType<Cliente>
    {
        public ClienteType(CaducaContext caducaContext, LocService locService)
        {

            Name = "Cliente";

            Field(c => c.Id).Description("Id");
            Field(c => c.Clave).Description("Clave del producto");
            Field(c => c.NombreComercial).Description("Nombre comercial del cliente");
            Field(c => c.RazonSocial).Description("Razón Social");

        }
    }
}
