using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
namespace eSaludCareAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Token"] != null)
            {
                return RedirectToAction("Usuarios", "Home");
            }

            return View();

        }
        public ActionResult Usuarios()
        {
            return View();
        }
        public ActionResult CerrarSesion()
        {
            Session.Clear(); 
            return RedirectToAction("Index", "Home"); 
        }


        [HttpPost]
        public JsonResult RegistrarSesion(string token, string nombre, string rol)
        {
            Session["jwt"] = token;
            Session["Usuario"] = nombre;
            Session["Rol"] = rol;
            return Json(new { success = true });
        }
    }
}