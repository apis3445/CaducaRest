using System.Linq;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;

namespace CaducaRest.GraphQL.HotChocolate;

/// <summary>
/// Querys for grapqhl
/// </summary>
public class Query
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="caducaContext"></param>
    /// <param name="locService"></param>
    /// <returns></returns>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Caducidad> GetCaducidad([Service] CaducaContext caducaContext, [Service] LocService locService)
    {
        CaducidadDAO caducidadDAO = new CaducidadDAO(caducaContext, locService);
        return caducidadDAO.ObtenerIQueryable();
    }

    /// <summary>
    /// Obtener Clientes
    /// </summary>
    /// <param name="contexto">Caduca context</param>
    /// <returns></returns>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Cliente> GetCliente([Service] CaducaContext contexto) => contexto.Cliente;

    /// <summary>
    /// Obtener Categorias
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Categoria> GetCategoria([Service] CaducaContext contexto) => contexto.Categoria;

    /// <summary>
    /// Obtener ClientesCategoria
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ClienteCategoria> GetClienteCategoria([Service] CaducaContext contexto) => contexto.ClienteCategoria;

}

/// <summary>
/// Objeto para los métodos disponibles
/// </summary>
public class QueryType : ObjectType<Query>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
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

/// <summary>
/// Cliente Type
/// </summary>
public class ClienteType : ObjectType<Cliente>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
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

/// <summary>
/// 
/// </summary>
public class CategoriaType : ObjectType<Categoria>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
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
            .Field(f => f.ClientesCategorias).UseDbContext<CaducaContext>()
            .Resolve((ctx) =>
            {
                return ctx.DbContext<CaducaContext>().ClienteCategoria;
            });
    }
}