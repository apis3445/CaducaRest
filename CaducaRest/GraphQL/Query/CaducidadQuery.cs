﻿using CaducaRest.DAO;
using CaducaRest.GraphQL.Types;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Query
{
    public class CaducidadQuery: ObjectGraphType
    {
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
