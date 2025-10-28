using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSaludCareUsers.Controllers
{
    public class CitasController : Controller
    {
        private readonly CN_Citas _cnCitas = new CN_Citas();
        private readonly CN_Medicos _cnMedicos = new CN_Medicos();

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult MisCitas()
        {
            return View();
        }

        // Listado de médicos
        public ActionResult ListarMedicos()
        {
            var listaMedicos = _cnMedicos.ListarMedicos();
            ViewBag.listaMedicos = listaMedicos;
            return View();
        }

        public JsonResult obtenerMedicos()
        {
            List<MedicoAsignado> oLista = new CN_Medicos().ListarMedicos();
            return Json(new { elemento = oLista, estado = true }, JsonRequestBehavior.AllowGet);
        }

        // Nueva acción para seleccionar horario
        public ActionResult SeleccionarHorario(int idMedico, DateTime fecha)
        {
            var horarios = _cnCitas.ObtenerHorariosDisponibles(idMedico, fecha);
            ViewBag.HorariosDisponibles = horarios;
            ViewBag.idMedico = idMedico;
            ViewBag.fecha = fecha;
            return View();
        }


        private ConectionBD conexion = new ConectionBD();



    }
}