using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Permite registrar las categorías de los productos
    /// que vende la empresa
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Id de la categoria.
        /// </summary>
        /// <value>El Id se incrementa automáticamente</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece la clave de la categoría
        /// </summary>
        /// <value>La clave de la categoria</value>
        [Required(ErrorMessage = "Required")]
        [Range(1,999, ErrorMessage = "Range")]
        public int Clave { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la categoría
        /// </summary>
        /// <value>El nombre de la categoría</value>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(80)")]
        public string Nombre { get; set; }
    }
}
