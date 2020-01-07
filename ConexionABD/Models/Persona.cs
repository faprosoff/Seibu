using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionDeSeibu.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-ZñÑáéíóúÁÉÍÓÚ]+[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$", ErrorMessage = "Nombre no válido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100)]
        public String Nombre { get; set; }

        [RegularExpression(@"^[A-ZñÑáéíóúÁÉÍÓÚ]+[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$", ErrorMessage = "Apellido no válido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(100)]
        public String Apellido { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Dni { get; set; }

        [StringLength(255)]
        public String Telefono { get; set; }

        [EmailAddress(ErrorMessage = "E-mail no válido")]
        [StringLength(255)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(255)]
        public String Direccion { get; set; }
    }
}