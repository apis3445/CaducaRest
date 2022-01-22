using System.Linq;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace CaducaRest.GraphQL.HotChocolate
{
    /// <summary>
    /// Querys for grapqhl
    /// </summary>
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Caducidad> GetCaducidad([Service] CaducaContext caducaContext, [Service] LocService locService) {
            CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
            return (IQueryable<Caducidad>)caducidadDAO.ObtenerIQueryable();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Cliente> GetCliente([Service] CaducaContext contexto) => contexto.Cliente;

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Categoria> GetCategoria([Service] CaducaContext contexto) => contexto.Categoria;

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ClienteCategoria> GetClienteCategoria([Service] CaducaContext contexto) => contexto.ClienteCategoria;

    }

    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            /*descriptor
                .Field(c => c.GetCliente(default)).UseProjection()
                .Type<ClienteType>();
                */
            descriptor.Field("Cliente")
            .UseDbContext<CaducaContext>()
            .Resolve((ctx) =>
            {
                return ctx.DbContext<CaducaContext>().Cliente;
            });
        }
    }

    public class ClienteType : ObjectType<Cliente>
    {
        protected override void Configure(IObjectTypeDescriptor<Cliente> descriptor)
        {
            descriptor
                .Field(f => f.Id)
                .Type<IntType>();
            descriptor
                .Field(f => f.NombreComercial)
                .Type<StringType>();
        }
    }

    public class CategoriaType : ObjectType<Categoria>
    {
        protected override void Configure(IObjectTypeDescriptor<Categoria> descriptor)
        {
            descriptor
                .Field(f => f.Id)
                .Type<IntType>();
            descriptor
                .Field(f => f.Clave)
                .Type<StringType>();
            descriptor
                .Field(f => f.Nombre)
                .Type<StringType>();
            descriptor
                .Field(f=> f.ClientesCategorias).UseDbContext<CaducaContext>()
                .Resolve((ctx) =>
                {
                    return ctx.DbContext<CaducaContext>().ClienteCategoria;
                });
        }
    }
}
