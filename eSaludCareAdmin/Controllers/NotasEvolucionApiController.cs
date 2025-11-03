using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaNegocio;
using CapaEntidad;

namespace eSaludCareAdmin.Controllers
{
    [RoutePrefix("api/v1/notas_evolucion")]
    public class NotasEvolucionApiController : ApiController
    {
        private readonly CN_NotaEvolucion _notasEvolucion = new CN_NotaEvolucion();
        [HttpPost]
        [Route("crear")]

        public HttpResponseMessage Crear([FromBody] NotasEvolucion n)
        {
            try
            {
                bool resultado = _notasEvolucion.Guardar(n);
                return Request.CreateResponse(HttpStatusCode.OK, new { success = resultado });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("historial/{id_historial:int}")]
        public HttpResponseMessage ObtenerPorHistorial(int id_historial)
        {
            try
            {
                var lista = _notasEvolucion.ListarPorHistorial(id_historial);
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

}
