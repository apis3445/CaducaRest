using System;
using System.Linq;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using HotChocolate;
using HotChocolate.Types;

namespace CaducaRest.GraphQL.HotChocolate
{
    /// <summary>
    /// Querys for grapqhl
    /// </summary>
    public class Query
    {
        //[UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Caducidad> GetCaducidad([Service] CaducaContext contexto) => contexto.Caducidad;

        //[UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Categoria> GetCategoria([Service] CaducaContext contexto) => contexto.Categoria;

        //[UseProjection]
        [UseFiltering]
        [UseSorting]
        [UseSelection]
        public IQueryable<ClienteCategoria> GetClienteCategoria([Service] CaducaContext contexto) => contexto.ClienteCategoria;

    }

   
    public class ClienteCategoriaType : ObjectType<ClienteCategoria>
    {
        protected override void Configure(IObjectTypeDescriptor<ClienteCategoria> descriptor)
        {
            descriptor
                .Field(f => f.Cliente)
                
                .Type<ClienteType>();
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
}
