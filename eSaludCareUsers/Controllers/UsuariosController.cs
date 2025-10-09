/*using eSaludCareUsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


namespace eSaludCareUsers.Controllers
{
    public class UsuariosController : ApiController
    {
        private readonly AppDbContext _context = new AppDbContext();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/usuarios")]
        public IHttpActionResult ObtenerUsuarios()
        {
            var pacientes = _context.Pacientes
                .Select(p => new PerfilUsuario
                {
                    Id = p.Id,
                    Nombre = p.Nombres + " " + p.Apellido,
                    Telefono = p.Telefono,
                    Rol = "Paciente"
                }).ToList();

            var medicos = _context.Medicos
                .Select(m => new PerfilUsuario
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Telefono = m.Telefono,
                    Rol = "Medico"
                }).ToList();

            var perfiles = pacientes.Concat(medicos).ToList();
            return Ok(perfiles);
        }

    }
}

*/