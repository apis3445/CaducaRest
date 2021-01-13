using System;
namespace CaducaRest.Core
{
    /// <summary>
    /// Tipo de acción a realizar ejemplo, insertar, modificar
    /// </summary>
    public enum ActividadEnumeration
    {
        /// <summary>
        /// Borrar
        /// </summary>
        Borrar      = -1,
        /// <summary>
        /// Agregar
        /// </summary>
        Insertar    = 1,
        /// <summary>
        /// Actualizar
        /// </summary>
        Modificar    = 2,
        /// <summary>
        /// Autorizar
        /// </summary>
        Autorizar   = 3,
        /// <summary>
        /// Cancelar
        /// </summary>
        Cancelar    = 4 
    }
}
