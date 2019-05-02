using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Models
{
    /// <summary>
    /// Permite registrar la caducidad de los productos
    /// </summary>
    public class Caducidad
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the producto identifier.
        /// </summary>
        /// <value>The producto identifier.</value>
        [Required(ErrorMessage = "Required")]       
        public int ProductoId { get; set; }

        /// <summary>
        /// Gets or sets the cliente identifier.
        /// </summary>
        /// <value>The cliente identifier.</value>
        [Required(ErrorMessage = "Required")]
        public int ClienteId { get; set; }

        /// <summary>
        /// Gets or sets the cantidad.
        /// </summary>
        /// <value>The cantidad.</value>
        [Required(ErrorMessage = "Required")]
        public int Cantidad { get; set; }
    }
}