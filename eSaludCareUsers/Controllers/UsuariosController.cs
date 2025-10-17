using eSaludCareUsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eSaludCareUsers.Data;


namespace eSaludCareUsers.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        [Route("")]
        public IHttpActionResult ObtenerUsuarios()
        {
            var pacientes = _context.Pacientes
                .Select(p => new PerfilUsuario
                {
                    Id = p.id_usuario,
                    Nombre = p.Usuario.nombre + " " + p.Usuario.apellido,
                    Telefono = p.Usuario.telefono,
                    Correo = p.Usuario.correo,
                    Rol = "paciente"
                });
            var medicos = _context.Medicos
                .Select(m => new PerfilUsuario
                {
                    Id = m.id_usuario,
                    Nombre = m.Usuario.nombre + " " + m.Usuario.apellido,
                    Telefono = m.Usuario.telefono,
                    Correo = m.Usuario.correo,
                    Rol = "medico"
                });

            var admins = _context.Usuarios
                .Where(u => u.rol == "admin")
                .Select(u => new PerfilUsuario
                {
                    Id = u.id_usuario,
                    Nombre = u.nombre + " " + u.apellido,
                    Telefono = u.telefono,
                    Correo = u.correo,
                    Rol = "admin"
                });

            var perfiles = pacientes
                .Concat(medicos)
                .Concat(admins)
                .ToList();

            return Ok(perfiles);
        }





        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/usuarios/testconexion")]
        public IHttpActionResult TestConexion()
        {
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings["BDpsql"]?.ConnectionString;
            if (conn == null)
                throw new Exception("La cadena de conexión 'BDpsql' no fue encontrada.");

            return Ok("Cadena de conexión encontrada correctamente.");
        }


    }
}

