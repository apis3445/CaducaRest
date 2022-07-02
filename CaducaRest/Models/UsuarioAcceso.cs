using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Registra los accesos de los usuarios
/// </summary>
public class UsuarioAcceso
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
    /// Fecha de acceso
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Token de acceso
    /// </summary>
    [Column(TypeName = "VARCHAR(500)")]
    [Required(ErrorMessage = "Required")]
    public string Token { get; set; }

    /// <summary>
    /// Indica si el token esta activo
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public bool Activo { get; set; }

    /// <summary>
    /// Sistema operativo del cual acceso el usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [Column(TypeName = "VARCHAR(200)")]
    public string SistemaOperativo { get; set; }

    /// <summary>
    /// Navegado del cual acceso el usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [Column(TypeName = "VARCHAR(200)")]
    public string Navegador { get; set; }

    /// <summary>
    /// Ciudad de donde accedió el usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [StringLength(300, ErrorMessage = "StringLength")]
    [Column(TypeName = "VARCHAR(300)")]
    public string Ciudad { get; set; }

    /// <summary>
    /// Estado de donde accedio el usuario
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [StringLength(300, ErrorMessage = "StringLength")]
    [Column(TypeName = "VARCHAR(300)")]
    public string Estado { get; set; }

    /// <summary>
    /// Código para refrescar el token
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [Column(TypeName = "VARCHAR(200)")]
    public string RefreshToken { get; set; }

    /// <summary>
    /// Fecha en que se refresco el token
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public DateTime FechaRefresh { get; set; }

    /// <summary>
    /// Indica si el token se mantendrá en sesión mas tiempo
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public bool MantenerSesion { get; set; }

}
