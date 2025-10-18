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
            return View();
        }
        public ActionResult Usuarios()
        {
            return View();
        }
        public ActionResult CerrarSesion()
        {
            Session.Clear(); // Borra todos los datos de sesión
            return RedirectToAction("Index", "Home"); // Redirige al inicio
        }

    }
}