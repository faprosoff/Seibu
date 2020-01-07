using ConexionABD.Models;
using GestionDeSeibu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConexionABD.Controllers
{
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CrearPago(int id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult CrearPago(Pago pago)
		{
			Database db = new Database();

			if (ModelState.IsValid)
			{
				db.InsertarPago(pago);
                return RedirectToAction("ListarPagosAlumno", new { id = pago.Id });
            }
			else
			{
				return View();
			}
		}

        // LISTAR PAGOS DE UN ALUMNO

        public ActionResult ListarPagosAlumno(int id)
        {
            Database db = new Database();
            double saldo = db.DameSaldoAlumno(id);
            ViewBag.saldo = saldo;
            Alumno alumno = db.BuscarAlumnoPorId(id);
            ViewBag.nombreAlumno = alumno.Nombre;
            ViewBag.apellidoAlumno = alumno.Apellido;
            var alumnos = db.ObtenerPagosDelAlumno(id);
            return View(alumnos);
        }
    }
}