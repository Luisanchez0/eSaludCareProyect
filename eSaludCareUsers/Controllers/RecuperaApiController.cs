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
    public class RecuperaApiController : ApiController
    {
        [HttpPost]
        [Route("recuperar-clave")]
        public IHttpActionResult RecuperarClave([FromBody] string correo)
        {
            if (string.IsNullOrEmpty(correo))
                return BadRequest("El correo electrónico es obligatorio.");

            CN_Recursos recurso = new CN_Recursos();
            bool resultado = recurso.RecuperarClave(correo);

            if (resultado) { 
                CN_Correos.EnviarCambioContrasena(correo);
                return Ok(new { mensaje = "Se ha enviado una nueva clave temporal a tu correo." });
            }
            else
                return BadRequest("No se pudo recuperar la clave. Verifica el correo o inténtalo más tarde.");
        }
        [HttpPost]
        [Route("cambiar-contrasena")]
        public IHttpActionResult CambiarContrasena([FromBody] CambioClave model)
        {
            if (model == null || string.IsNullOrEmpty(model.NuevaClave) || model.IdUsuario <= 0)
                return BadRequest("Datos inválidos para cambiar la contraseña.");

            CN_Usuarios cnUsuarios = new CN_Usuarios();
            bool resultado = cnUsuarios.CambiarContrasena(model.IdUsuario, model.NuevaClave);

            if (resultado)
            {
                // Envía correo de confirmación si se cambió correctamente
                CN_Correos.EnviarCambioContrasena(model.Correo);
                return Ok(new { mensaje = "La contraseña se cambió correctamente y se envió una confirmación al correo." });
            }
            else
            {
                return BadRequest("No se pudo actualizar la contraseña. Inténtalo más tarde.");
            }
        }



    }
}
