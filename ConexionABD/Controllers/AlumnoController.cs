using GestionDeSeibu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionDeSeibu.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            return View();
        }

        // CREAR ALUMNO

        public ActionResult CrearAlumno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearAlumno(Alumno alumno)
        {
            Database db = new Database();

            Alumno ADni = db.BuscarAlumnoPorDNI(alumno.Dni);
            Alumno A_NroSocio = db.BuscarAlumnoPorNroSocio(alumno.NroSocio);

            if (ADni == null && A_NroSocio == null)
            {
                if (ModelState.IsValid)
                {
                    db.InsertarAlumno(alumno);
                    return RedirectToAction("ListarAlumnos");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                if (ADni != null)
                {
                    ModelState.AddModelError("Dni", "El DNI ingresado ya existe.");
                }
                if (A_NroSocio != null)
                {
                    ModelState.AddModelError("NroSocio", "El Número de Socio ingresado ya existe.");
                }
                return View();
            }
        }

        // MODIFICAR ALUMNO

        public ActionResult ModificarAlumno(int id)
        {
            Database db = new Database();
            Alumno alumno = db.BuscarAlumnoPorId(id);
            return View(alumno);
        }

        [HttpPost]
        public ActionResult ModificarAlumno(Alumno alumno)
        {
            Database db = new Database();

            if (ModelState.IsValid)
            {
                db.ModificarAlumno(alumno);
                return RedirectToAction("ListarAlumnos");
            }
            else
            {
                return View();
            }
        }

        // LISTAR ALUMNOS

        public ActionResult ListarAlumnos()
        {
            Database db = new Database();
            var alumnos = db.ObtenerTodosLosAlumnos();
            return View(alumnos);
        }

        [HttpPost]
        public PartialViewResult BuscarAlumnoAjax(Alumno alumno)
        {
            Database db = new Database();
            try
            {
                List<Alumno> alumnos = db.BuscarAlumnosPorCriterios(alumno);
                return PartialView("ResultadosAlumnosAjax", alumnos);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public PartialViewResult BuscarCursoAjax(FormCollection form)
        {
            String nomProf = form["profesor"];
            String nomCurso = form["nombre"];
            TimeSpan horario;
            TimeSpan.TryParse(form["horario"], out horario);
            Database db = new Database();
            try
            {
                List<Curso> cursos = db.BuscarCursosPorCriterios(nomProf, nomCurso, horario);
                return PartialView("ResultadosCursosAjax", cursos);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}