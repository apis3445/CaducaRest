using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    public class Tabla
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(40, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(40)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(40, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(200)")]
        public string Descripción { get; set; }
    }
}
