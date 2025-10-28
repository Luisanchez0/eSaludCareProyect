using CapaEntidad; 
using eSaludCareUsers.Data;
using eSaludCareUsers.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace eSaludCareUsers.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        [Route("")]
        public IHttpActionResult ObtenerUsuarios(string rol, int idUsuario)
        {
            if (rol == "admin")
            {
                // Devuelve todos los usuarios
                var perfiles = _context.Usuarios
                    .Select(u => new PerfilUsuarioDTO
                    {
                        Id = u.id_usuario,
                        Nombre = u.nombre + " " + u.apellido,
                        Telefono = u.telefono,
                        Correo = u.correo,
                        Rol = u.rol
                    })
                    .ToList();

                return Ok(perfiles);
            }

            if (rol == "medico")
            {
                // Devuelve solo los pacientes asignados al médico
                var pacientes = _context.Pacientes
                    .Where(p => p.IdMedico == idUsuario) // ← relación directa
                    .Select(p => new PerfilUsuarioDTO
                    {
                        Id = p.id_usuario,
                        Nombre = p.Usuario.nombre + " " + p.Usuario.apellido,
                        Telefono = p.Usuario.telefono,
                        Correo = p.Usuario.correo,
                        Rol = "paciente"
                    })
                    .ToList();

                return Ok(pacientes);
            }

            return Unauthorized(); // otros roles no tienen acceso
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

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Correo) || string.IsNullOrEmpty(request.Contrasena))
                return BadRequest("Datos de login incompletos.");

            var usuario = _context.Usuarios.FirstOrDefault(u => u.correo == request.Correo);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.contrasena))
                return Unauthorized();
            
            return Ok(new
            {
                token = Guid.NewGuid().ToString(), // o JWT si lo implementas después
                id_usuario = usuario.id_usuario,
                rol = usuario.rol,
                nombre = usuario.nombre
            });
        }


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult ObtenerPorId(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            var dto = new PerfilUsuarioDTO
            {
                Id = usuario.id_usuario,
                Nombre = usuario.nombre + " " + usuario.apellido,
                Correo = usuario.correo,
                Telefono = usuario.telefono,
                Rol = usuario.rol
            };

            return Ok(dto);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CrearUsuario(Usuarios usuario)
        {
            if (usuario == null)
                return BadRequest("Datos incompletos.");

            if (string.IsNullOrEmpty(usuario.correo) || string.IsNullOrEmpty(usuario.contrasena))
                return BadRequest("Correo y contraseña son obligatorios.");

            var existe = _context.Usuarios.Any(u => u.correo == usuario.correo);
            if (existe)
                return Conflict();

            usuario.contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.contrasena);
            usuario.fecha_registro = DateTime.Now;
            usuario.fecha_actualizacion = DateTime.Now;
            usuario.token = Guid.NewGuid().ToString();

            var entidad = UsuarioAdapter.Convertir(usuario);
            _context.Usuarios.Add(entidad);
            _context.SaveChanges();

            // 👨‍⚕️ Si el rol es médico, registrar también en la tabla "medicos"
            if (usuario.rol.ToLower() == "medico")
            {
                
               var medico = new eSaludCareUsers.Models.Medico
                {
                    id_usuario = entidad.id_usuario,
                    especialidad = usuario.especialidad,
                    numero_cedula = usuario.numero_cedula
                };

                _context.Medicos.Add(medico);
                _context.SaveChanges();
            }

            return Ok(new { mensaje = "Usuario creado correctamente", id = entidad.id_usuario });
        }


        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult EditarUsuario(int id, Usuarios usuario)
        {
            if (usuario == null || id != usuario.id_usuario)
                return BadRequest("Datos inválidos.");

            var entidad = _context.Usuarios.Find(id);
            if (entidad == null)
                return NotFound();

            // Actualiza los campos
            entidad.nombre = usuario.nombre;
            entidad.apellido = usuario.apellido;
            entidad.correo = usuario.correo;
            entidad.telefono = usuario.telefono;
            entidad.rol = usuario.rol;
            entidad.fecha_actualizacion = DateTime.Now;

            if (usuario.rol == "medico")
            {
                //entidad.especialidad = usuario.especialidad;
                //entidad.numero_cedula = usuario.numero_cedula;

                var medico = _context.Medicos.FirstOrDefault(m => m.id_usuario == id);
                if (medico != null)
                {
                    medico.especialidad = usuario.especialidad;
                    medico.numero_cedula = usuario.numero_cedula;
                }
                else
                {
                    _context.Medicos.Add(new eSaludCareUsers.Models.Medico
                    {
                        id_usuario = id,
                        especialidad = usuario.especialidad,
                        numero_cedula = usuario.numero_cedula
                    });
                }
            }
                _context.SaveChanges();

            return Ok(new { mensaje = "Usuario actualizado correctamente", id = entidad.id_usuario });
        }

        [HttpDelete]
        [Route("usuarios/{id}")]
        public IHttpActionResult EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok(new { mensaje = "Usuario eliminado correctamente", id });
        }

        [HttpPut]
        [Route("usuarios/cambiar-rol/{id}")]
        public IHttpActionResult CambiarRol(int id, [FromBody] dynamic payload)
        {
            string nuevoRol = payload?.rol;
            if (string.IsNullOrEmpty(nuevoRol))
                return BadRequest("Rol inválido.");

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            usuario.rol = nuevoRol;
            usuario.fecha_actualizacion = DateTime.Now;
            _context.SaveChanges();

            return Ok(new { mensaje = "Rol actualizado correctamente", id });
        }

        [HttpPut]
        [Route("usuarios/reset-password/{id}")]
        public IHttpActionResult ResetPassword(int id, [FromBody] dynamic payload)
        {
            string nuevaContrasena = payload?.contrasena;
            if (string.IsNullOrEmpty(nuevaContrasena))
                return BadRequest("Contraseña inválida.");

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            usuario.contrasena = nuevaContrasena;
            usuario.fecha_actualizacion = DateTime.Now;
            _context.SaveChanges();

            return Ok(new { mensaje = "Contraseña actualizada correctamente", id });
        }




    }
}

