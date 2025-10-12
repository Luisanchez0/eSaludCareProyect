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

        public JsonResult usuariosListados()
        {
            List<CapaEntidad.Usuarios> olista = new List<CapaEntidad.Usuarios>();
            olista = new CapaNegocio.CN_Usuarios().Listar();

            return Json(olista,JsonRequestBehavior.AllowGet);

        }
    }
}