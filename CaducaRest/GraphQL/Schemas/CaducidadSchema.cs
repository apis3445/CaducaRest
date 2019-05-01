using CaducaRest.GraphQL.Query;
using GraphQL;
using GraphQL.Types;

namespace CaducaRest.GraphQL.Schemas
{
    public class CaducidadSchema : Schema
    {
        public CaducidadSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CaducidadQuery>();
        }
    }
}
