using GestionDeSeibu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConexionABD.Controllers
{
    public class CursoController : Controller
    {
        // GET: Curso
        public ActionResult Index()
        {
            return View();
        }

        // CREAR CURSO

        public ActionResult CrearCurso()
        {
            Database db = new Database();
            ViewBag.data = db.ObtenerTodosLosProfesores();
            return View();
        }

        [HttpPost]
        public ActionResult CrearCurso(Curso curso, FormCollection collection)
        {
            Database db = new Database();
            int idProfesor = Convert.ToInt32(collection["idProfesor"]);
            curso.Profesor = db.BuscarProfesorPorId(idProfesor);
            if (ModelState.IsValid)
            {
                db.InsertarCurso(curso);
                return RedirectToAction("ListarCursos");
            }
            else
            {
                ViewBag.data = db.ObtenerTodosLosProfesores();
                return View();
            }
        }

        // MODIFICAR CURSO

        public ActionResult ModificarCurso(int id)
        {
            Database db = new Database();
            Curso curso = db.BuscarCursoPorId(id);
            ViewBag.data = db.ObtenerTodosLosProfesores();
            return View(curso);
        }

        [HttpPost]
        public ActionResult ModificarCurso(Curso curso, FormCollection collection)
        {
            Database db = new Database();

            int idProfesor = Convert.ToInt32(collection["idProfesor"]);
            curso.Profesor = db.BuscarProfesorPorId(idProfesor);

            if (ModelState.IsValid)
            {
                db.ModificarCurso(curso);
                return RedirectToAction("ListarCursos");
            }
            else
            {
                ViewBag.data = db.ObtenerTodosLosProfesores();
                return View();
            }
        }

        // LISTAR CURSOS

        public ActionResult ListarCursos()
        {
            Database db = new Database();
            var cursos = db.ObtenerTodosLosCursos();
            return View(cursos);
        }

        // ASIGNAR ALUMNOS A CURSOS

        public ActionResult AsignarAlumnos(int id)
        {
            Database db = new Database();
            Curso curso = db.BuscarCursoPorId(id);
            ViewBag.data = db.ObtenerTodosLosAlumnos();

            return View(curso);
        }

        [HttpPost]
        public ActionResult AsignarAlumnos(FormCollection form)
        {
            return RedirectToAction("ListarCursos");
        }

        public ActionResult AsignarAlumnosNew()
        {
            return View("AsignarAlumnosNew");
        }

        //MOSTRAR ALUMNOS DE UN CURSO

        public ActionResult MostrarAlumnosCurso(int id)
        {
            Database db = new Database();
            ViewBag.idCurso = id;
            Curso curso = db.BuscarCursoPorId(id);
            ViewBag.nombreCurso = curso.Nombre;
            ViewBag.nombreProfesorCurso = curso.Profesor.Nombre;
            ViewBag.apellidoProfesorCurso = curso.Profesor.Apellido;
            List<Alumno> Alumnos = db.ObtenerAlumnosDelCurso(id);
            return View(Alumnos);
        }

        [HttpPost]
        public JsonResult ValidarYAsignar(FormCollection form)
        {
            int idCurso = Convert.ToInt32(form["id_curso"]);
            int idAlum = Convert.ToInt32(form["id_alum"]);
            Database db = new Database();
            Curso c = db.BuscarCursoPorId(idCurso);
            Alumno a = db.BuscarAlumnoPorId(idAlum);
            if (a != null && c != null && c.TieneCupo())
            {
                try
                {
                    db.InsertarRelacionCursoAlumno(c, a);
                }
                catch (Exception e)
                {
                    return Json("El alumno ya existe en el curso.");
                }
                return Json("El alumno se asignó correctamente.");
            }
            else
            {
                return Json("El curso no tiene cupo.");
            }
        }

        // ELIMINAR ALUMNOS DE UN CURSO

        public ActionResult EliminarAlumnoDeCurso(int idAlumno, int idCurso)
        {
            Database db = new Database();
            Alumno a = db.BuscarAlumnoPorId(idAlumno);
            Curso c = db.BuscarCursoPorId(idCurso);
            List<Alumno> Alumnos;
            try
            {
                if (a != null && c != null)
                {
                    bool eliminado = db.EliminarAlumnoDeCurso(a, c);
                    if (eliminado)
                    {
                        Alumnos = db.ObtenerAlumnosDelCurso(idCurso);
                        ViewBag.idCurso = idCurso;
                        ViewBag.nombreCurso = c.Nombre;
                        ViewBag.nombreProfesorCurso = c.Profesor.Nombre;
                        ViewBag.apellidoProfesorCurso = c.Profesor.Apellido;
                        return View("MostrarAlumnosCurso", Alumnos);
                    }
                    else
                    {
                        Alumnos = db.ObtenerAlumnosDelCurso(idCurso);
                        ViewBag.idCurso = idCurso;
                        ViewBag.nombreCurso = c.Nombre;
                        ViewBag.nombreProfesorCurso = c.Profesor.Nombre;
                        ViewBag.apellidoProfesorCurso = c.Profesor.Apellido;
                        return View("MostrarAlumnosCurso", Alumnos);
                    }
                }
                else
                {
                    Alumnos = db.ObtenerAlumnosDelCurso(idCurso);
                    ViewBag.idCurso = idCurso;
                    ViewBag.nombreCurso = c.Nombre;
                    ViewBag.nombreProfesorCurso = c.Profesor.Nombre;
                    ViewBag.apellidoProfesorCurso = c.Profesor.Apellido;
                    return View("MostrarAlumnosCurso", Alumnos);
                }
            }
            catch
            {
                Alumnos = db.ObtenerAlumnosDelCurso(idCurso);
                ViewBag.idCurso = idCurso;
                ViewBag.nombreCurso = c.Nombre;
                ViewBag.nombreProfesorCurso = c.Profesor.Nombre;
                ViewBag.apellidoProfesorCurso = c.Profesor.Apellido;
                return View("MostrarAlumnosCurso", Alumnos);
            }
        }
    }
}