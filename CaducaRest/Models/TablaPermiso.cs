using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Indica los permisos que tiene una tabla
    /// </summary>
    public class TablaPermiso
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id de la tabla a la que se le agregan los permisos
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int TablaId { get; set; }

        /// <summary>
        /// Permisos que se van a validar en la tabla
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int PermisoId { get; set; }
    }
}
