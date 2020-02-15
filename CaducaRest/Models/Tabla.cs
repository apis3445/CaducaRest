using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Registra todas las tablas del sistema para registrar
    /// el historial de cambios o los permisos 
    /// </summary>
    public class Tabla
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la tabla
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [StringLength(40, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(40)")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción de la tabla
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [StringLength(40, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(200)")]
        public string Descripción { get; set; }
    }
}