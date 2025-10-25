using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace eSaludCareUsers.Views.Login
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GuardarSesion(string token, string nombre, string rol)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { success = false, message = "Token inválidos." });
            }
            Session["Token"] = token;
            Session["Nombre"] = nombre;
            Session["Rol"] = rol;
            return Json(new { success = true });

        }
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }




    }
}
