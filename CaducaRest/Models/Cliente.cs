using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CaducaRest.Rules;

namespace CaducaRest.Models
{
    /// <summary>
    /// Guarda los clientes
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CaducaRest.Models.Cliente"/> class.
        /// </summary>
        public Cliente() => ClientesCategorias = new Collection<ClienteCategoria>();
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
        [RequiredWithField("RazonSocial")]
        public string RFC { get; set; }

        /// <summary>
        /// Razón social del cliente 
        /// como esta registrado ante hacienda
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Column(TypeName = "VARCHAR(250)")]
        [Rules.Cliente.RazonSocialValidation()]
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

        /// <summary>
        /// Para mostrar las categorias de los clientes
        /// </summary>
        public  ICollection<ClienteCategoria> ClientesCategorias { get; set; }

        /// <summary>
        /// Para mostrar las caducidades
        /// </summary>
        public virtual ICollection<Caducidad> Caducidades { get; set; }

    }
}
