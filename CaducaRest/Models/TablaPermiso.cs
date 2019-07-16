using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    public class TablaPermiso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public int TablaId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int PermisoId { get; set; }
    }
}
