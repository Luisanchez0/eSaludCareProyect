using System.Collections.Generic;
using System.Web.Http;
using CapaNegocio;
using CapaEntidad;

namespace eSaludCareAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("api/citas")]
    public class ConsultasCitasApiController : ApiController
    {
        private readonly CN_ConsultasCitas negocio;

        public ConsultasCitasApiController()
        {
            string cadenaConexion = "Host=localhost;Port=5432;Database=clinica_db;Username=postgres;Password=";
            negocio = new CN_ConsultasCitas(cadenaConexion);
        }

        // GET: api/citas/agendadas
        [HttpGet]
        [Route("agendadas")]
        public IHttpActionResult GetCitasAgendadas()
        {
            List<CitaAgendadaDTO> citas = negocio.ObtenerCitasAgendadas();

            if (citas == null || citas.Count == 0)
                return NotFound();

            return Ok(citas);
        }
    }
}