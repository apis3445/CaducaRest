using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{


    /// <summary>
    /// Permite registrar las categorías de los productos
    /// que vende la empresa
    /// </summary>
    [Display(Name = "Categoría")]
    public class Categoria
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CaducaRest.Models.Categoria"/> class.
        /// </summary>
        public Categoria() => ClientesCategorias = new Collection<ClienteCategoria>();

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
        [StringLength(80)]
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(80)")]
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the clientes categorias.
        /// </summary>
        /// <value>The clientes categorias.</value>
        public virtual ICollection<ClienteCategoria> ClientesCategorias { get; set; }
    }
}
