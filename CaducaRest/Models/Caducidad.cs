using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Models
{
    public class Caducidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]       
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Required")]
        public int Cantidad { get; set; }
    }
}