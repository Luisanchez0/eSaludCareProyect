using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace eSaludCareUsers.ApiControllers
{
    [RoutePrefix("api/v1/paciente")]
    public class PerfilPacienteApiController : ApiController
    {
        private readonly CN_Pacientes _cnPacientes = new CN_Pacientes();
        private readonly CN_Usuarios _cnUsuarios = new CN_Usuarios();
        private readonly CN_Tokens _cnTokens = new CN_Tokens();
        /*
         *         [HttpGet]

        [Route("perfil")]
        public IHttpActionResult ObtenerPerfil(int idUsuario)
        {
            try
            {
                var usuario = _cnUsuarios.ObtenerUsuarioPorId(idUsuario);
                var paciente = _cnPacientes.ObtenerPacientePorUsuario(idUsuario);

                if (usuario == null || paciente == null)
                {
                    return NotFound();
                }
                var perfil = new
                {
                    usuario.IdUsuario,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Correo,
                    usuario.Telefono,
                    paciente.FechaNacimiento,
                    paciente.Genero,
                    paciente.Direccion,
                };
                return Ok(perfil);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        */


        [HttpGet]
        [Route("perfil")]
        public IHttpActionResult ObtenerPerfil()
        {
            try
            {
                string token = ObtenerTokenHeader();
                if (string.IsNullOrEmpty(token))
                    return Content(System.Net.HttpStatusCode.Unauthorized, "Token no proporcionado.");

                int idUsuario = _cnTokens.ObtenerIdUsuarioPorToken(token);
                if (idUsuario <= 0)
                    return Content(System.Net.HttpStatusCode.Unauthorized, "Token inválido.");

                var usuario = _cnUsuarios.ObtenerUsuarioPorId(idUsuario);
                var paciente = _cnPacientes.ObtenerPacientePorUsuario(idUsuario);
                if (usuario == null || paciente == null)
                {
                    return NotFound();
                }
                var perfil = new
                {
                    usuario.IdUsuario,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Correo,
                    usuario.Telefono,
                    paciente.FechaNacimiento,
                    paciente.Genero,
                    paciente.Direccion,
                };
                return Ok(perfil);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPut]
        [Route("actualizarPerfil")]
        public IHttpActionResult ActualizarPerfil([FromBody] PerfilPaciente perfil)
        {
            try
            {
                string token = ObtenerTokenHeader();
                if (string.IsNullOrEmpty(token))
                    return Content(System.Net.HttpStatusCode.Unauthorized, "Token no proporcionado.");
                int idUsuario = _cnTokens.ObtenerIdUsuarioPorToken(token);
                if (idUsuario <= 0)
                    return Content(System.Net.HttpStatusCode.Unauthorized, "Token inválido.");

                perfil.IdUsuario = idUsuario;
                bool actualizado = _cnPacientes.ActualizarPerfilPaciente(perfil);
                if (actualizado)
                {
                    return Ok("Perfil actualizado correctamente.");
                }
                else
                {
                    return BadRequest("No se pudo actualizar el perfil.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        // Método auxiliar para leer el token del encabezado
        private string ObtenerTokenHeader()
        {
            IEnumerable<string> authHeader;
            if (Request.Headers.TryGetValues("Authorization", out authHeader))
            {
                return authHeader.FirstOrDefault()?.Replace("Bearer ", "").Trim();
            }
            return null;
        }






    }
}
