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
using System.Threading.Tasks;
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
                // Validación de datos obligatorios
                if (nuevaCita == null ||
                    nuevaCita.Fecha == default(DateTime) ||
                    nuevaCita.Hora == default(TimeSpan) ||
                    nuevaCita.IdMedico <= 0 ||
                    nuevaCita.IdPaciente <= 0 ||
                    string.IsNullOrEmpty(nuevaCita.Motivo))
                {
                    return BadRequest("Todos los campos son obligatorios.");
                }

                DateTime fechaCita = nuevaCita.Fecha;
                TimeSpan hora = nuevaCita.Hora;

                // Validar rango de horarios (9:00 a 17:00, intervalos de 30 minutos)
                var horaMin = new TimeSpan(9, 0, 0);  // 9:00
                var horaMax = new TimeSpan(17, 0, 0); // 17:00

                if (hora < horaMin || hora > horaMax || hora.Minutes % 30 != 0)
                {
                    return BadRequest("El horario debe estar entre 9:00 y 17:00 en intervalos de 30 minutos.");
                }

                // Verificar si el horario ya está ocupado
                using (var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BDpsql"].ToString()))
                {
                    con.Open();
                    string checkSql = @"
                        SELECT COUNT(*) 
                        FROM citas 
                        WHERE id_medico = @idMedico 
                        AND fecha = @fecha 
                        AND hora = @hora 
                        AND estado IN ('pendiente', 'confirmada', 'realizada')";

                    using (var checkCmd = new NpgsqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.AddWithValue("@idMedico", nuevaCita.IdMedico);
                        checkCmd.Parameters.AddWithValue("@fecha", fechaCita.Date);
                        checkCmd.Parameters.AddWithValue("@hora", hora);

                        object countObj = checkCmd.ExecuteScalar();
                        int count = countObj != DBNull.Value ? Convert.ToInt32(countObj) : 0;

                        if (count > 0)
                        {
                            return BadRequest("El horario seleccionado ya está ocupado.");
                        }
                    }
                }

                // Registrar cita
                bool resultado = _citaNegocio.RegistrarCita(nuevaCita);
                if (resultado)
                    return Ok(new { mensaje = "Cita registrada correctamente." });
                else
                    return BadRequest("No se pudo registrar la cita.");
            }
            catch (Exception ex)
            {
                // Retornar error con excepción real
                return InternalServerError(ex);
            }
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

        [HttpGet]
        [Route("citas/paciente/{idPaciente}")]
        public IHttpActionResult ObtenerCitasPaciente(int idPaciente)
        {
            using (var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BDpsql"].ToString()))
            {
                con.Open();
                string query = @"
                    SELECT id_cita, id_medico, estado, motivo, fecha, hora, fecha_registro
                    FROM citas
                    WHERE id_paciente = @idPaciente
                    ORDER BY fecha DESC";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("idPaciente", idPaciente);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var lista = new List<object>();

                        while (reader.Read())
                        {
                            lista.Add(new
                            {
                                id_cita = reader["id_cita"],
                                id_medico = reader["id_medico"],
                                estado = reader["estado"],
                                motivo = reader["motivo"],
                                fecha = reader["fecha"],
                                hora = reader["hora"],
                                fecha_registro = reader["fecha_registro"]
                            });
                        }

                        return Ok(lista);
                    }
                }
            }
        }

        //apartado mis citas

        [HttpGet]
        [Route("misCitas")]
        public IHttpActionResult ObtenerMisCitas([FromUri] int idUsuario)
        {
            try
            {
                using (var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BDpsql"].ToString()))
                {
                    con.Open();

                    string query = @"
                SELECT 
                    c.id_cita,
                    c.fecha,
                    c.hora,
                    c.estado,
                    CONCAT(u.nombre, ' ', u.apellido) AS doctor
                FROM citas c
                INNER JOIN medicos m ON c.id_medico = m.id_medico
                INNER JOIN usuarios u ON m.id_usuario = u.id_usuario
                INNER JOIN pacientes p ON c.id_paciente = p.id_paciente
                WHERE p.id_usuario = @idUsuario
                ORDER BY c.fecha DESC, c.hora ASC";

                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (var reader = cmd.ExecuteReader())
                        {
                            var citas = new List<CitasVista>();

                            while (reader.Read())
                            {
                                citas.Add(new CitasVista
                                {
                                    IdCita = Convert.ToInt32(reader["id_cita"]),
                                    Fecha = Convert.ToDateTime(reader["fecha"]).ToString("yyyy-MM-dd"),
                                    Hora = reader["hora"].ToString(),
                                    Doctor = reader["doctor"].ToString(),
                                    Estado = reader["estado"].ToString()
                                });
                            }

                            return Ok(citas); // ✅ ya no hay error de serialización
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("confirmarCita/{idCita}")]
        public IHttpActionResult ConfirmarCita(int idCita)
        {
            return CambiarEstadoCita(idCita, "confirmada");
        }

        [HttpPut]
        [Route("cancelarCita/{idCita}")]
        public IHttpActionResult CancelarCita(int idCita)
        {
            return CambiarEstadoCita(idCita, "cancelada");
        }

        // 🔹 Método auxiliar para actualizar estado
        private IHttpActionResult CambiarEstadoCita(int idCita, string nuevoEstado)
        {
            try
            {
                using (var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BDpsql"].ToString()))
                {
                    con.Open();

                    string sql = @"UPDATE citas SET estado = @estado WHERE id_cita = @idCita";

                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@idCita", idCita);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                            return Ok(new { mensaje = $"Cita {nuevoEstado} correctamente." });
                        else
                            return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




    }
}
*/ 