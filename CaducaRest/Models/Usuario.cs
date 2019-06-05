using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(15, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(15)")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(255, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(255)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool Activo { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Adicional1 { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(200, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(200)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(80, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(80)")]
        public string Email { get; set; }
    }
}
