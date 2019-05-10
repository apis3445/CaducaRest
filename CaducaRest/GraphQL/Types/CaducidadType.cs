using CaducaRest.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.GraphQL.Types
{
    public class CaducidadType : ObjectGraphType<Caducidad>
    {
        public CaducidadType()
        {
            Field(a => a.Id);
            Field(a => a.ProductoId);
            Field(a => a.ClienteId);
            Field(a => a.Cantidad);
            Field(a => a.Fecha);
        }

    }
}
