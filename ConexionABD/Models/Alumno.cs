using ConexionABD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionDeSeibu.Models
{
    public class Alumno : Persona
    {
        public bool EsSocio { get; set; }
        public int NroSocio { get; set; }
    }
}