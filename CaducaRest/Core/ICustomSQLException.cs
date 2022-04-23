
using Microsoft.Data.SqlClient;

namespace CaducaRest.Core
{
    /// <summary>
	/// Custom SQL Exception interface
	/// </summary>
    public interface ICustomSQLException
    {
        /// <summary>
		/// Shows a more friendly error
		/// </summary>
		/// <param name="sqlError">SQl Excepption</param>
		/// <param name="tablaActual">Tabla con el error</param>
		/// <param name="claseOrigen">Clase con el error</param>
		/// <returns></returns>
        string MuestraErrorMYSQL(SqlException sqlError, string tablaActual, string claseOrigen);
    }
}