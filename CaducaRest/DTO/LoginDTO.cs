using System.ComponentModel.DataAnnotations;

namespace CaducaRest.DTO
{
    /// <summary>
    /// Datos para el login
    /// </summary>
    public class LoginDTO
    {

        /// <summary>
        /// Usuario
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [StringLength(15)]
        public string Usuario { get; set; }

        /// <summary>
        /// Password del usuario
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [StringLength(255)]
        public string Password { get; set; }

        /// <summary>
        /// Código para desbloquear el usuario
        /// </summary>
        public int Codigo { get; set; }

    }
}