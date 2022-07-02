using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Para guardar los errores
/// </summary>
public class Errores
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Fecha del error
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    public string Mensaje { get; set; }
}

