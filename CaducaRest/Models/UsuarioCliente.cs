using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Registra los clientes a los que tiene acceso un usuario
/// </summary>
public class UsuarioCliente
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
    /// Id del cliente al que tiene acceso el usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int ClienteId { get; set; }
}
