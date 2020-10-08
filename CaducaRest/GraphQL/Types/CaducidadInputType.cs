using GraphQL.Types;

namespace CaducaRest.GraphQL.Types
{
    /// <summary>
    /// Caducidad input type.
    /// </summary>
    public class CaducidadInputType: InputObjectGraphType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CaducaRest.GraphQL.Types.CaducidadInputType"/> class.
        /// </summary>
        public CaducidadInputType()
        {
            Name = "CaducidadInput";
            Field<NonNullGraphType<IntGraphType>>("Id");
            Field<NonNullGraphType<IntGraphType>>("Cantidad");
            Field<NonNullGraphType<IntGraphType>>("ProductoId");
            Field<NonNullGraphType<IntGraphType>>("ClienteId");
            Field<NonNullGraphType<DateGraphType>>("Fecha");
        }
    }
}
