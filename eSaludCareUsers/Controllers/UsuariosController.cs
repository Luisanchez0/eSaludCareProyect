using eSaludCareUsers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using eSaludCareUsers.Data;


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
                    Id = p.id_paciente,
                    Nombre = p.nombres + " " + p.apellidos,
                    Telefono = p.telefono,
                    Rol = "Paciente"
                }).ToList();

            var medicos = _context.Medicos
                .Select(m => new PerfilUsuario
                {
                    Id = m.id_medico,
                    Nombre = m.nombres + " " + m.apellidos,
                    Telefono = m.telefono,
                    Rol = "Medico"
                }).ToList();

            var perfiles = pacientes.Concat(medicos).ToList();
            return Ok(perfiles);
        }

    }
}

