using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace eSaludCareAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ConsultasAgendadasController : Controller
    {
        private readonly CN_ConsultasCitas negocio;

        public ConsultasAgendadasController()
        {
            string cadenaConexion = "Host=localhost;Port=5432;Database=clinica_db;Username=postgres;Password=1234";
            negocio = new CN_ConsultasCitas(cadenaConexion);
        }

        // GET: ConsultasAgendadas
        public ActionResult Index()
        {
            List<CitaAgendadaDTO> citas = negocio.ObtenerCitasAgendadas();
            return View(citas);
        }
    }
}
