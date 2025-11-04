using CapaEntidad;
using CapaNegocio;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;



namespace eSaludCareAdmin.Controllers
{
    [RoutePrefix("api/v1")]
    public class RecuperarApiController : ApiController
    {
        [HttpPost]
        [Route("recuperar-clave")]
        public IHttpActionResult RecuperarClave([FromBody] string correo)
        {
            if (string.IsNullOrEmpty(correo))
                return BadRequest("El correo electrónico es obligatorio.");

            CN_Recursos recurso = new CN_Recursos();
            bool resultado = recurso.RecuperarClave(correo);

            if (resultado)
                return Ok(new { mensaje = "Se ha enviado una nueva clave temporal a tu correo." });
            else
                return BadRequest("No se pudo recuperar la clave. Verifica el correo o inténtalo más tarde.");
        }

    }
}