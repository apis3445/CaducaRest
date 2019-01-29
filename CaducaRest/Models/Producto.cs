using CaducaRest.Rules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Registra los productos 
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id de la categoría
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Clave del producto
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Range(1, 999, ErrorMessage = "Range")]
        public int Clave { get; set; }

        /// <summary>
        /// Nombre del producto
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(80)")]
        [NombreValidation()]
        public string Nombre { get; set; }
    }
}
