using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Registra los permisos que tiene un rol en la tabla
/// </summary>
public class RolTablaPermiso
{
    /// <summary>
    /// Id 
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Permiso que tiene la tabla 
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int TablaPermisoId { get; set; }

    /// <summary>
    /// Id del rol
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public int RolId { get; set; }

    /// <summary>
    /// Indica si el rol tiene permiso o no
    /// </summary>
    [Required(ErrorMessage = "Required")]
    public bool TienePermiso { get; set; }
}