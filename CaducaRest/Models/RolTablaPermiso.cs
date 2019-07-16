using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    public class RolTablaPermiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public int TablaPermisoId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool TienePermiso { get; set; }
    }
}
