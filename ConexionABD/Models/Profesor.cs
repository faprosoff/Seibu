using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionDeSeibu.Models
{
    public class Profesor : Persona
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public double PorcentajePago { get; set; }
    }
}