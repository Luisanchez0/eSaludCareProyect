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
            string cadenaConexion = "Host=localhost;Port=5432;Database=Clinica_db;Username=postgres;Password=tu_contraseña";
            negocio = new CN_ConsultasCitas(cadenaConexion);
        }

        // GET: api/citas/agendadas
        [HttpGet]
        [Route("agendadas")]
        public IHttpActionResult GetCitasAgendadas()
        {
            var citas = negocio.ObtenerCitasAgendadas();
            if (citas == null || citas.Count == 0)
                return NotFound();

            return Ok(citas);
        }

        // GET: api/citas/agendadas/{id}
        [HttpGet]
        [Route("agendadas/{id:int}")]
        public IHttpActionResult GetCitaPorId(int id)
        {
            var cita = negocio.ObtenerCitaPorId(id);
            if (cita == null)
                return NotFound();

            return Ok(cita);
        }

        // POST: api/citas/agendadas
        [HttpPost]
        [Route("agendadas")]
        public IHttpActionResult CrearCita([FromBody] CitaAgendadaDTO nuevaCita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = negocio.CrearCitaAgendada(nuevaCita);
            return Ok(resultado);
        }

        // PUT: api/citas/agendadas/{id}
        [HttpPut]
        [Route("agendadas/{id:int}")]
        public IHttpActionResult ActualizarCita(int id, [FromBody] CitaAgendadaDTO citaActualizada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = negocio.ActualizarCitaAgendada(id, citaActualizada);
            return Ok(resultado);
        }

        // DELETE: api/citas/agendadas/{id}
        [HttpDelete]
        [Route("agendadas/{id:int}")]
        public IHttpActionResult EliminarCita(int id)
        {
            var resultado = negocio.EliminarCitaAgendada(id);
            if (!resultado)
                return NotFound();

            return Ok("Cita eliminada correctamente.");
        }
    }
}