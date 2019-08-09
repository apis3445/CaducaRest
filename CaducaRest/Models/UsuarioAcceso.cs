using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    public class UsuarioAcceso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "VARCHAR(400)")]
        [Required(ErrorMessage = "Required")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool Activo { get; set; }

        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(200)")]
        public string SistemaOperativo { get; set; }

        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(200)")]
        public string Navegador { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(300, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(300)")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(300, ErrorMessage = "StringLength")]
        [Column(TypeName = "VARCHAR(300)")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(200)")]
        public string RefreshToken { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime FechaRefresh { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool MantenerSesion { get; set; }
        
    }
}
