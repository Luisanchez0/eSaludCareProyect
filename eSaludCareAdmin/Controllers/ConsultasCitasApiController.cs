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
        private CN_ConsultasCitas negocio = new CN_ConsultasCitas();

        [HttpGet]
        [Route("agendadas")]
        public IHttpActionResult GetCitasAgendadas()
        {
            var citas = negocio.ObtenerCitasAgendadas();
            return Ok(citas);
        }
    }
}