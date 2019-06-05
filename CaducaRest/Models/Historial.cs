using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    public class Historial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public int TabladId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int OrigenId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int Actividad { get; set; }

        [Required(ErrorMessage = "Required")]
        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime FechaHora { get; set; }

        [StringLength(250, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(250)")]
        public string Observa { get; set; }
    }
}
