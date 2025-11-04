using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaDatos;
using CapaNegocio;


namespace eSaludCareAdmin.Controllers
{
    public class PacientesApiController : ApiController
    {
        private readonly CN_Pacientes _pacienteNegocio = new CN_Pacientes();

        [HttpGet]
        [Route("api/v1/pacientes/listar")]
        public IHttpActionResult ListarPacientes()
        {
            try
            {
                var pacientes = _pacienteNegocio.Listar();
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
