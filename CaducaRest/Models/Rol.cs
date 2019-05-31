using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(50)")]
        public string Nombre { get; set; }
    }
}
