namespace CaducaRest.Core
{
    /// <summary>
    /// Permite agregar reglas
    /// </summary>
    public interface IRegla
    {
        /// <summary>
        /// Mensaje de error
        /// </summary>
        CustomError customError { get; set; }

        /// <summary>
        /// Permite validar una regla
        /// </summary>
        /// <returns></returns>
        bool ValidarRegla();
    }
}
