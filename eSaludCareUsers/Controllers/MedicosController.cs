using eSaludCareUsers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eSaludCareUsers.Controllers
{
    public class MedicosController : ApiController
    {
        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        [Route("api/medicos")]
        public IHttpActionResult ObtenerMedicos()
        {
            var medicos = _context.Medicos
                .Select(m => new
                {
                    Id = m.id_medico,
                    Nombre = m.nombres + " " + m.apellidos,
                    Especialidad = m.especialidad,
                    Telefono = m.telefono,
                    Correo = m.correo
                })
                .ToList();

            return Ok(medicos);

        }
    }
}
