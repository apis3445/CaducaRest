using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CaducaRest.Core
{
    /// <summary>
    /// Permite registrar las diferentes operaciones que se realizan
    /// en la base de datos
    /// </summary>
    public static class Operaciones
    {
        /// <summary>
        /// Crear registros
        /// </summary>
        public static OperationAuthorizationRequirement Crear = new
                OperationAuthorizationRequirement
        { Name = "Crear" };
        /// <summary>
        /// Actualizar registros
        /// </summary>
        public static OperationAuthorizationRequirement Modificar = new
                OperationAuthorizationRequirement
        { Name = "Modificar" };
        /// <summary>
        /// Borrar registros
        /// </summary>
        public static OperationAuthorizationRequirement Borrar = new
                OperationAuthorizationRequirement
        { Name = "Borrar" };
        /// <summary>
        /// Consultar registros
        /// </summary>
        public static OperationAuthorizationRequirement Consultar = new
                OperationAuthorizationRequirement
        { Name = "Consultar" };
    }
}