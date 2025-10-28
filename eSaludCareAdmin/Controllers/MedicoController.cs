using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eSaludCareUsers.Data;

namespace eSaludCareAdmin.Controllers
{
    [Authorize] 
    [RoutePrefix("api/v1/medicos")]
    public class MedicoController : ApiController
    {

        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult GetMedicos()
        {
            var medicos = _context.Medicos
                .Where(m => m.Usuario.rol == "medico")
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

