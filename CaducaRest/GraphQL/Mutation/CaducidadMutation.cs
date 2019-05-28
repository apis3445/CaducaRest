using System;
using CaducaRest.DAO;
using CaducaRest.GraphQL.Types;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Mutation
{
    public class CaducidadMutation : ObjectGraphType
    {
        public CaducidadMutation(CaducaContext caducaContext, LocService locService)
        {
            CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);

            Field<CaducidadType>
                (
                    "createCaducidad",
                    arguments: new QueryArguments(
                        new QueryArgument
                        <NonNullGraphType<CaducidadInputType>> { Name = "caducidad" }
                    ),
                    resolve: context =>
                    {
                        var caducidad = context.GetArgument<Caducidad>("caducidad");
                        var correcto = caducidadDAO.AgregarAsync(caducidad).Result;
                        return caducidad;
                    }
                );
            Field<StringGraphType>(
                    "deleteCaducidad",
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                    resolve: context =>
                    {
                        var id = context.GetArgument<int>("id");
                        var caducidad = caducidadDAO.BorraAsync(id).Result;
                        return $"La caducidad con el id: {id} fue borrada correctamente";
                    }

                    );
        }
    }
}
