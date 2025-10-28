using CapaDatos;   // Ajusta según tu DbContext
using eSaludCareUsers.Data; 
using eSaludCareUsers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace eSaludCareUsers.Controllers
{
    [RoutePrefix("api/v1")]
    public class HorariosDisponiblesController : ApiController
    {
        private readonly AppDbContext _context;

        private ConectionBD conexion = new ConectionBD();

        public HorariosDisponiblesController()
        {
            _context = new AppDbContext(); 
        }

        // GET api/v1/HorariosDisponibles?idMedico=1&fecha=2025-10-26
        [Route("HorariosDisponibles")]
        public async Task<IHttpActionResult> GetHorariosDisponibles(int idMedico, string fecha)
        {
            if (!DateTime.TryParse(fecha, out DateTime fechaCita))
                return BadRequest("Formato de fecha inválido");

            // Buscar médico
            var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.id_medico == idMedico);
            if (medico == null)
                return Ok(new { estado = false, mensaje = "Médico no encontrado" });

            // Preparar horarios (soportar propiedades TimeSpan no-nullable o nullable)
            TimeSpan? horaInicio = medico.hora_inicio;
            TimeSpan? horaFin = medico.hora_fin;
            var duracion = medico.duracion_turno_minutos > 0 ? medico.duracion_turno_minutos : 30;

            if (!horaInicio.HasValue || !horaFin.HasValue || horaInicio.Value >= horaFin.Value)
                return Ok(new { estado = false, mensaje = "El médico no tiene horario configurado o el horario es inválido" });

            // Obtener citas existentes del día (asumiendo que CitaMedica.Hora es TimeSpan)
            var citasExistentes = await _context.Citas
                .Where(c => c.id_medico == idMedico && DbFunctions.TruncateTime(c.fecha) == fechaCita.Date)
                .Select(c => c.hora)
                .ToListAsync();

            // Crear conjunto de horas ocupadas para búsqueda rápida
            var horasOcupadas = new HashSet<TimeSpan>(citasExistentes);

            // Generar horarios del turno del médico
            var horarios = new List<object>();
            var horaActual = horaInicio.Value;

            while (horaActual < horaFin.Value)
            {
                bool ocupado = horasOcupadas.Contains(horaActual);

                horarios.Add(new
                {
                    hora = horaActual.ToString(@"hh\:mm"),
                    ocupado = ocupado
                });

                horaActual = horaActual.Add(TimeSpan.FromMinutes(duracion));
            }

            if (!horarios.Any())
                return Ok(new { estado = false, mensaje = "No hay horarios disponibles" });

            return Ok(new
            {
                estado = true,
                horarios
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);
        }
    }
}