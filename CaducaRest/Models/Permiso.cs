using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Permisos que puede tener el sistema
    /// </summary>
    /// <example>Agregar</example>
    public class Permiso
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Clave del permiso
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int Clave { get; set; }

        /// <summary>
        /// Nombre del permiso
        /// </summary>
        [Column(TypeName = "VARCHAR(100)")]
        public string Nombre { get; set; }
    }
}
