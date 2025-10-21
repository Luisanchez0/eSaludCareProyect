using System.Collections.Generic;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

namespace eSaludCareUsers.Controllers
{
    [Authorize(Roles = "admin,medico")]
    public class ConsultasPacientesController : Controller
    {
        private CN_ConsultasPacientes negocio = new CN_ConsultasPacientes();

        // GET: ConsultasPacientes
        public ActionResult Index()
        {
            List<PacienteConsultaDTO> pacientes = negocio.ObtenerPacientesRegistrados();
            return View(pacientes);
        }
    }
}