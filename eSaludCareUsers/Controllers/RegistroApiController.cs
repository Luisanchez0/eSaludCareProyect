using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using CapaEntidad;
using CapaNegocio;

namespace eSaludCareUsers.Controllers
{

    [RoutePrefix("api/v1")]
    public class RegistroApiController : ApiController
    {
        private readonly CN_Usuarios _usuarioNegocio = new CN_Usuarios();
        [HttpPost]
        [Route("registro")]
        public IHttpActionResult Registrar([FromBody] UsuarioEntidad nuevoUsuario)
        {
            if (nuevoUsuario == null)
                return BadRequest("Datos inválidos.");

            if (string.IsNullOrEmpty(nuevoUsuario.Nombre) ||
                string.IsNullOrEmpty(nuevoUsuario.Apellido) ||
                string.IsNullOrEmpty(nuevoUsuario.Correo) ||
                string.IsNullOrEmpty(nuevoUsuario.Contrasena))
            {
                return BadRequest("Todos los campos obligatorios deben ser completados.");
            }

            try
            {
                bool registrado = _usuarioNegocio.RegistrarUsuario(nuevoUsuario);

                if (registrado)
                    return Ok(new { mensaje = "Usuario registrado correctamente." });
                else
                    return BadRequest("No se pudo registrar el usuario. El correo podría estar en uso.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



    }

        
}
