using Microsoft.Data.SqlClient;

namespace CaducaRest.Core
{
    public interface ICustomSQLException
    {
        string MuestraErrorMYSQL(SqlException sqlError, string tablaActual, string claseOrigen);
    }
}