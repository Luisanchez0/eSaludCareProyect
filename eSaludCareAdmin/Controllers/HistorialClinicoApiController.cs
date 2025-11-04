using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaEntidad;


namespace eSaludCareAdmin.Controllers
{
    [RoutePrefix("api/v1/historial_clinico")]
    public class HistorialClinicoApiController : ApiController
    {
        private readonly CN_HistorialClinico _historial = new CN_HistorialClinico();

        [HttpPost]
        [Route("crear")]
        public HttpResponseMessage Crear([FromBody] HistorialClinico h)
        {
            try
            {
                bool resultado = _historial.Guardar(h);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = resultado });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("paciente/{id_paciente:int}")]
        public HttpResponseMessage ObtenerPorPaciente(int id_paciente)
        {
            try
            {
                var lista = _historial.ListarPorPaciente(id_paciente);
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
