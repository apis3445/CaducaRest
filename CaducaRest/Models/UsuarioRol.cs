using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Roles que tiene el usuario
    /// </summary>
    public class UsuarioRol
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id del usuario
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Id del rol
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int RolId { get; set; }
    }
}
