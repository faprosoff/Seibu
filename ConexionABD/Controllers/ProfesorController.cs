using GestionDeSeibu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionDeSeibu.Controllers
{
    public class ProfesorController : Controller
    {
        // GET: Profesor
        public ActionResult Index()
        {
            return View();
        }

        // CREAR PROFESOR

        public ActionResult CrearProfesor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearProfesor(Profesor profesor)
        {
            Database db = new Database();

            if (db.BuscarProfesorPorDNI(profesor.Dni) == null)
            {
                if (ModelState.IsValid)
                {
                    db.InsertarProfesor(profesor);
                    return RedirectToAction("ListarProfesores");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("Dni", "El Dni ya existe.");
                return View();
            }
        }

        // MODIFICAR PROFESOR

        public ActionResult ModificarProfesor(int id)
        {
            Database db = new Database();
            Profesor profesor = db.BuscarProfesorPorId(id);
            return View(profesor);
        }

        [HttpPost]
        public ActionResult ModificarProfesor(Profesor profesor)
        {
            Database db = new Database();

            if (ModelState.IsValid)
            {
                try
                {
                    db.ModificarProfesor(profesor);
                }
                catch (Exception e)
                {
                    return View();
                }
                return RedirectToAction("ListarProfesores");
            }
            else
            {
                return View();
            }
        }

        // LISTAR PROFESORES

        public ActionResult ListarProfesores()
        {
            Database db = new Database();
            var profesores = db.ObtenerTodosLosProfesores();
            return View(profesores);
        }

    }
}