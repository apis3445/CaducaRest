using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Registra las categorías a las que tiene acceso un usuario
    /// </summary>
    public class UsuarioCategoria
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
        /// Id de la categoría a la que tiene acceso
        /// el usuario
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public int CategoriaId { get; set; }
    }
}
