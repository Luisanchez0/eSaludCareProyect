using eSaludCareUsers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CapaEntidad;

namespace eSaludCareUsers.Controllers
{
    public class MedicosController : ApiController
    {
        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        [Route("api/medicos")]
        public IHttpActionResult ObtenerMedicos([FromUri] string nombre = null, [FromUri] string especialidad = null)
        {
            var query = _context.Medicos
                .Where(m => m.Usuario.rol == "medico");

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(m => m.Usuario.nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(especialidad))
                query = query.Where(m => m.especialidad.Contains(especialidad));

            var medicos = query
                .Select(m => new MedicoDTO
                {
                    IdUsuario = m.id_usuario,
                    Nombre = m.Usuario.nombre,
                    Apellido = m.Usuario.apellido,
                    Correo = m.Usuario.correo,
                    Telefono = m.Usuario.telefono,
                    FechaRegistro = m.Usuario.fecha_registro,
                    FechaActualizacion = m.Usuario.fecha_actualizacion,
                    Especialidad = m.especialidad,
                    NumeroCedula = m.numero_cedula
                })
                .ToList();

            return Ok(medicos);
        }

    }

}
