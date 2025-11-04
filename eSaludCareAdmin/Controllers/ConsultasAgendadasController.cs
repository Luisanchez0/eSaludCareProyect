using CapaEntidad;
using CapaNegocio;
using System.Collections.Generic;
using System.Web.Mvc;

namespace eSaludCareAdmin.Controllers
{
    public class ConsultasAgendadasController : Controller
    {
        private readonly CN_ConsultasCitas negocio;

        public ConsultasAgendadasController()
        {
            string cadenaConexion = "Host=localhost;Port=5432;Database=clinica_db;Username=postgres;Password=123456";
            negocio = new CN_ConsultasCitas(cadenaConexion);
        }

        public ActionResult Index()
        {
            // Validación manual del rol
            if (Session["Rol"]?.ToString() != "admin")
            {
                return RedirectToAction("Index", "Login");
            }

            List<CitaAgendadaDTO> citas = negocio.ObtenerCitasAgendadas();
            return View(citas);
        }
    }
}