using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models;

/// <summary>
/// Roles que puede tener un usuario
/// </summary>
public class Rol
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Nombre del rol
    /// </summary>
    [Required(ErrorMessage = "Required")]
    [Column(TypeName = "VARCHAR(50)")]
    public string Nombre { get; set; }
}

