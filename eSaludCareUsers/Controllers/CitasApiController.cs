using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eSaludCareUsers.Controllers
{
    [RoutePrefix("api/v1")]
    public class CitasApiController : ApiController
    {
        private readonly CN_Citas _citaNegocio = new CN_Citas();
        [HttpPost]
        [Route("regitrarCita")]

        public IHttpActionResult Registrar([FromBody] CitaMedica nuevaCita)
        {
            if (nuevaCita == null)
                return BadRequest("Datos de cita no válidos.");

            nuevaCita.Estado = "pendiente";

            if (nuevaCita.IdPaciente <= 0 ||
                nuevaCita.IdMedico <= 0 ||
                nuevaCita.FechaCita == default(DateTime) ||
                string.IsNullOrEmpty(nuevaCita.Motivo))
            {
                return BadRequest("Todos los campos obligatorios deben ser completados.");
            }

            try
            {
                bool citaRegistrada = _citaNegocio.RegistrarCita(nuevaCita);
                if (citaRegistrada)
                    return Ok(new { mensaje = "Cita registrada correctamente." });
                else
                    return BadRequest("No se pudo registrar la cita.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
