using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaNegocio;
using CapaEntidad;

namespace eSaludCareUsers.Controllers
{
    [RoutePrefix("api/v1/HistMedic")]
    public class HistMedicApiController : ApiController
    {
        private readonly CN_HistorialMedico objetoCN = new CN_HistorialMedico();

        [HttpGet]
        [Route("paciente/{id_paciente}")]
        public IHttpActionResult ObtenerPorPaciente(int id_paciente)
        {
            try
            {
                var lista = objetoCN.ListarPorPaciente(id_paciente);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("crear")]
        public IHttpActionResult CrearHistorialMedico([FromBody] HistorialMedico historial)
        {
            try
            {
                bool resultado = objetoCN.Guardar(historial);
                if (resultado)
                {
                    return Ok(new { mensaje = "Historial médico creado exitosamente." });
                }
                else
                {
                    return BadRequest("No se pudo crear el historial médico.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult ActualizarHistorialMedico([FromBody] HistorialMedico historial)
        {
            try
            {
                bool resultado = objetoCN.Actualizar(historial);
                if (resultado)
                {
                    return Ok(new { mensaje = "Historial médico actualizado exitosamente." });
                }
                else
                {
                    return BadRequest("No se pudo actualizar el historial médico.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



    }
}