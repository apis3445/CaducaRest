using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        /// Para acceder a los datos de un producto
        /// </summary>
        public Producto Producto { get; set; }

        /// <summary>
        /// Gets or sets the cliente Id.
        /// </summary>
        /// <value>The cliente identifier.</value>
        [Required(ErrorMessage = "Required")]
        public int ClienteId { get; set; }

        /// <summary>
        /// Guarda los clientes
        /// </summary>
        public Cliente Cliente { get; set; }

        /// <summary>
        /// Gets or sets la cantidad.
        /// </summary>
        /// <value>The cantidad.</value>
        [Required(ErrorMessage = "Required")]
        public int Cantidad { get; set; }

        /// <summary>
        /// Fecha en que caducan los articulos
        /// </summary>
        /// <value>The fecha.</value>
        [Required(ErrorMessage = "Required")]
        public DateTime Fecha { get; set; }
    }
}