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

    public class UsuarioApiController : ApiController
    {
        private CN_Usuarios cnUsuarios = new CN_Usuarios();

        // GET: api/v1/usuarios
        [HttpGet]
        [Route("usuarios")]
        public IHttpActionResult ObtenerUsuarios()
        {
            var lista = cnUsuarios.ListarUsuarios(); // Este método lo creamos en el paso siguiente
            return Ok(lista);
        }

        // POST: api/v1/usuarios
        [HttpPost]
        [Route("usuarios")]
        public IHttpActionResult CrearUsuario([FromBody] UsuarioEntidad usuario)
        {
            bool resultado = cnUsuarios.RegistrarUsuario(usuario);
            return Ok(resultado);
        }

        // PUT: api/v1/usuarios/{id}
        [HttpPut]
        [Route("usuarios/{id}")]
        public IHttpActionResult ActualizarUsuario(int id, [FromBody] UsuarioEntidad usuario)
        {
            usuario.IdUsuario = id;
            bool resultado = cnUsuarios.ActualizarUsuario(usuario); // Lo creamos también
            return Ok(resultado);
        }

        // DELETE: api/v1/usuarios/{id}
        [HttpDelete]
        [Route("usuarios/{id}")]
        public IHttpActionResult EliminarUsuario(int id)
        {
            bool resultado = cnUsuarios.EliminarUsuario(id); // Lo creamos también
            return Ok(resultado);
        }

    }
}
