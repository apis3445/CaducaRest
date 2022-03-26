using System;
namespace CaducaRest.DTO
{
    /// <summary>
    /// Clase para el manejo de los tokens
    /// </summary>
    public class TokenDTO
    {
        /// <summary>
        /// Token generado.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Fecha de expiración del token.
        /// </summary>
        public DateTime TokenExpiration { get; set; }

        /// <summary>
        /// Número de control del usuario en sesión.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Nombre Usuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Codigo para refrescar el token
        /// </summary>
        public string RefreshToken { get; set; }
    }

}
