using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSaludCareAdmin.Controllers
{
    public class HistorialClinicoController : Controller
    {
        // GET: HistorialClinico
        public ActionResult HistorialClínico()
        {
            return View();
        }

        public ActionResult NotasEvolucion()
        {
            return View();
        }
        
    }
}
