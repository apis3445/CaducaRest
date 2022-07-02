using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Categorías de producto por cada cliente
/// </summary>
public class ClienteCategoria
{
    /// <summary>
    /// Id 
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Id del cliente
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int ClienteId { get; set; }

    /// <summary>
    /// Datos del cliente
    /// </summary>
    public Cliente Cliente { get; set; }

    /// <summary>
    /// Id de la categoria
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int CategoriaId { get; set; }

    /// <summary>
    /// Datos de la Categoría
    /// </summary>
    public Categoria Categoria { get; set; }
}

