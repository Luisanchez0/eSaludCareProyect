using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;


namespace eSaludCareUsers.Controllers
{
    public class CitasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar()
        {

            return View();
        }


        //listado de medicos
        private CN_Medicos _cnMedicos = new CN_Medicos();

        public ActionResult ListarMedicos()
        {
            var listaMedicos = _cnMedicos.ListarMedicos();

            ViewBag.listaMedicos = listaMedicos;
            return View();

            //return Json(new { data = listaMedicos }, JsonRequestBehavior.AllowGet);
        }

            public JsonResult obtenerMedicos()
            {
                List<MedicoAsignado> oLista = new List<MedicoAsignado>();
                oLista = new CN_Medicos().ListarMedicos();

                return Json(new { elemento = oLista, estado = true } ,JsonRequestBehavior.AllowGet);
            }

    }
}