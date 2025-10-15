using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eSaludCareAdmin.Controllers
{
    [Authorize] 
    [RoutePrefix("api/v1/medicos")]
    public class MedicoController : ApiController
    {

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult GetMedicos()
        {
            // Ejemplo simple, luego puedes conectarlo con CapaNegocio
            return Ok(new
            {
                success = true,
                message = "Acceso autorizado",
                usuario = User.Identity.Name
            });
        }

    }
}
