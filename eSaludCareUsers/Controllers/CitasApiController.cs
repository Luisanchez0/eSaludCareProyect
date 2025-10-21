using CapaEntidad;
using CapaNegocio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace eSaludCareUsers.Controllers
{
    [RoutePrefix("api/v1")]
    public class CitasApiController : ApiController
    {
        private readonly CN_Citas _citaNegocio = new CN_Citas();
        [HttpPost]
        [Route("registrarCita")]

        public IHttpActionResult RegistrarCita([FromBody] CitaMedica nuevaCita)
        {
            try
            {
                if (nuevaCita == null)
                    return BadRequest("Datos de cita no válidos.");

                bool resultado = _citaNegocio.RegistrarCita(nuevaCita);
                if (resultado)
                    return Ok(new { mensaje = "Cita registrada correctamente." });
                else
                    return BadRequest("No se pudo registrar la cita.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /*
        public IHttpActionResult Registrar([FromBody] CitaMedica nuevaCita)
        {
            var userID = User.Identity.Name;//obtener id usuario desde el token
            if (nuevaCita == null)
                return BadRequest("Datos de cita no válidos.");

            nuevaCita.Estado = "pendiente";

            if (nuevaCita.IdPaciente <= 0 ||
                nuevaCita.IdMedico <= 0 ||
                nuevaCita.Fecha == default(DateTime) ||
                nuevaCita.Hora == default(TimeSpan) ||
                string.IsNullOrEmpty(nuevaCita.Motivo))
            {
                return BadRequest("Todos los campos obligatorios deben ser completados.");
            }

            try
            {
                bool citaRegistrada = _citaNegocio.RegistrarCita(nuevaCita);
                if (citaRegistrada)
                    return Ok(new { mensaje = "Cita registrada correctamente." });
                else
                    return BadRequest("No se pudo registrar la cita.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        */


        [HttpGet]
        [Route("HorariosDisponibles")]
        public IHttpActionResult HorariosDisponibles(int idMedico, DateTime fecha)
        {
            //definir horarios de atencion
            var horariosAtencion = new List<string>();
            {
                int inicio = 9;
                int fin = 17;
                int intervalo = 30; // minutos
                for (int hora = inicio; hora < fin; hora++)
                {
                    for (int min = 0; min < 60; min += intervalo)
                    {
                        horariosAtencion.Add($"{hora:D2}:{min:D2}");
                    }

                }

                //obtener citas ya agendadas
                var citasAgendadas = _citaNegocio.ListarCitas(idMedico, fecha)
                    .Select(c => c.Hora.ToString(@"hh\:mm"))
                    .ToList();

                //filtrar horarios disponibles
                var HorariosDisponibles = horariosAtencion.Except(citasAgendadas).ToList();

                return Ok(new { 
                    estado = true,
                    horarios = HorariosDisponibles } );
            }
            ;
        }


        [HttpGet]
        [Route("obtenerIdPaciente")]
        public IHttpActionResult ObtenerPacientePorUsuario(int idUsuario)
        {
            using (var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BDpsql"].ToString()))
            {
                con.Open();
                string query = "SELECT id_paciente FROM pacientes WHERE id_usuario = @idUsuario";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("idUsuario", idUsuario);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return Ok(new { idPaciente = Convert.ToInt32(result) });
                    else
                        return NotFound();
                }
            }
        }

    }
}
