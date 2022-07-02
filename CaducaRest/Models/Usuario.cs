using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Usuarios del sistema
/// </summary>
public class Usuario
{
    /// <summary>
    /// Id del usuario
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Clave del usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [StringLength(15, ErrorMessage = "StringLength")]
    [Column(TypeName = "VARCHAR(15)")]
    public string Clave { get; set; }

    /// <summary>
    /// Password del usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [StringLength(255, ErrorMessage = "StringLength")]
    [Column(TypeName = "VARCHAR(255)")]
    public string Password { get; set; }

    /// <summary>
    /// Indica si el usuario esta activo o no
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public bool Activo { get; set; }

    /// <summary>
    /// Dato aidicional para el usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public string Adicional1 { get; set; }

    /// <summary>
    /// Nombre del usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [StringLength(200, ErrorMessage = "StringLength")]
    [Column(TypeName = "VARCHAR(200)")]
    public string Nombre { get; set; }

    /// <summary>
    /// Email del usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [StringLength(80, ErrorMessage = "StringLength")]
    [Column(TypeName = "VARCHAR(80)")]
    public string Email { get; set; }

    /// <summary>
    /// Número de intentos de acceso equivocados 
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int Intentos { get; set; }

    /// <summary>
    /// Código de desbloqueo
    /// </summary>
    public int Codigo { get; set; }
}
