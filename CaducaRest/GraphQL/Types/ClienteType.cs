using System;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Types
{
    /// <summary>
    /// GraphQL tiene sus propios tipos de datos por lo que
    /// es necesario crear un objeto de tipo ObjectGraphType
    /// para mapear los campos del objeto con los de GraphQL
    /// </summary>
    public class ClienteType : ObjectGraphType<Cliente>
    {
        /// <summary>
        /// Constructor para mapear los campos
        /// </summary>
        /// <param name="locService">Para mensajes de error en varios idiomas</param>
        public ClienteType( LocService locService)
        {

            Name = "Cliente";
            Field(c => c.Id).Description("Id");
            Field(c => c.Clave).Description("Clave del producto");
            Field(c => c.NombreComercial).Description("Nombre comercial del cliente");
            Field(c => c.RazonSocial).Description("Razón Social");

        }
    }
}
