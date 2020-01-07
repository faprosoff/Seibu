using GestionDeSeibu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConexionABD.Models
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int Id { get; set; } // IdAlumno

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int NroFactura { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Detalle { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public double Cobro { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public double MontoPago { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime Fecha { get; set; }

        public double Saldo { get; set; }
    }
}