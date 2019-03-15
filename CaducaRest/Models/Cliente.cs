using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaducaRest.Models
{
    /// <summary>
    /// Guarda los clientes
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Id del cliente
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Clave del cliente
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Range(1, 999999, ErrorMessage = "Range")]
        [CaducaRest.Rules.Cliente.ClaveValidation()]
        public int Clave { get; set; }

        /// <summary>
        /// RFC del cliente
        /// </summary>
        [Column(TypeName = "VARCHAR(15)")]
        public string RFC { get; set; }

        /// <summary>
        /// Razón social del cliente 
        /// como esta registrado ante hacienda
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(250)")]
        [CaducaRest.Rules.Cliente.RazonSocialValidation()]
        public string  RazonSocial { get; set; }
        
        /// <summary>
        /// Nombre comercial del cliente
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(250)")]
        public string NombreComercial { get; set; }

        /// <summary>
        /// Dirección del cliente
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(200)")]
        public string Direccion { get; set; }

        /// <summary>
        /// Email de contacto
        /// </summary>
        [Column(TypeName = "VARCHAR(150)")]
        public string Email { get; set; }

        /// <summary>
        /// Teléfono Fijo del cliente
        /// </summary>
        [Column(TypeName = "VARCHAR(20)")]
        public string Telefono { get; set; }

        /// <summary>
        /// Número de celular del cliente
        /// </summary>
        [Column(TypeName = "VARCHAR(20)")]
        public string Celular { get; set; }

        /// <summary>
        /// Sitio Web del cliente
        /// </summary>
        [Column(TypeName = "VARCHAR(20)")]
        public string SitioWeb { get; set; }

        /// <summary>
        /// Indica si el cliente esta activo
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public bool Activo { get; set; }
    }
}
