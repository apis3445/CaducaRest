using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Guarda el historial de cambios
/// </summary>
public class Historial
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Id de la tabla
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int TablaId { get; set; }

    /// <summary>
    /// Id del registro que se modifico o agrego
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int OrigenId { get; set; }

    /// <summary>
    /// Indica la activicad que se realizo por ejemplo agregar
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int Actividad { get; set; }

    /// <summary>
    /// Id del usuaario que modifica o agrega un registro
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [ForeignKey("Usuario")]
    public int? UsuarioId { get; set; }

    /// <summary>
    /// Fecha y hora en que se modifica el registro
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public DateTime FechaHora { get; set; }

    /// <summary>
    /// Observaciones sobre la modificación realizada
    /// </summary>
    [StringLength(250, ErrorMessage = "StringLength")]
    [Column(TypeName = "VARCHAR(250)")]
    public string Observa { get; set; }
}
