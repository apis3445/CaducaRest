namespace CaducaRest.Core;

/// <summary>
/// Mensjaes de error 
/// </summary>
public class CustomError
{
    /// <summary>
    /// Código de error 
    /// </summary>
    public int StatusCode;

    /// <summary>
    /// Mensaje del error
    /// </summary>
    public string Message;

    /// <summary>
    /// Campo con el error
    /// </summary>
    public string Field;

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="statusCode">Código de erorr</param>
    /// <param name="message">Mensaje que explica el error</param>
    /// <param name="field">Campo que tiene el error</param>
    public CustomError(int statusCode, string message, string field = "")
    {
        StatusCode = statusCode;
        Message = message;
        Field = field;
    }

}
