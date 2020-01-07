using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionDeSeibu.Models
{
	public class Curso
	{

		public int IdCurso { get; set; }

		[RegularExpression(@"^[A-ZñÑáéíóúÁÉÍÓÚ]+[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$", ErrorMessage = "Nombre no válido")]
		[Required(ErrorMessage = "Este campo es obligatorio")]
		public String Nombre { get; set; }

		[Required(ErrorMessage = "Este campo es obligatorio")]
		public String Dia { get; set; }

		[Required(ErrorMessage = "Este campo es obligatorio")]
		public TimeSpan Horario { get; set; }

		[DataType(DataType.Currency)]
		[Required(ErrorMessage = "Este campo es obligatorio")]
		public double PrecioSocio { get; set; }

		[DataType(DataType.Currency)]
		[Required(ErrorMessage = "Este campo es obligatorio")]
		public double PrecioNoSocio { get; set; }

		public Profesor Profesor { get; set; }

		[Required(ErrorMessage = "Este campo es obligatorio")]
		public int CantTopeInscriptos { get; set; }

        // MÉTODOS

		public bool TieneCupo()
		{
			Database db = new Database();
			if (db.ObtenerCantAlumnosCurso(this.IdCurso) < this.CantTopeInscriptos)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}