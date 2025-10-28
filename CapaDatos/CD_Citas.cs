using CapaEntidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Citas
    {
        private ConectionBD conexion = new ConectionBD();


        public bool RegistrarCita(CitaMedica cita)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();

                //verificar eistencia de paciente
                using (var cmdVerificarPaciente = new NpgsqlCommand("SELECT COUNT(*) FROM pacientes WHERE id_paciente = @id_paciente", con))
                {
                    cmdVerificarPaciente.Parameters.AddWithValue("id_paciente", cita.IdPaciente);
                    var existePaciente = (long)cmdVerificarPaciente.ExecuteScalar() > 0;


                    if (!existePaciente)
                    {
                        throw new Exception("El paciente no existe.");
                    }

                }

                string query = @"INSERT INTO citas (id_paciente, id_medico, fecha, hora, estado, motivo, fecha_registro)
                 VALUES (@id_paciente, @id_medico, @fecha, @hora, @estado, @motivo, @fecha_registro)";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("hora", cita.Hora);
                    cmd.Parameters.AddWithValue("id_paciente", cita.IdPaciente);
                    cmd.Parameters.AddWithValue("id_medico", cita.IdMedico);
                    cmd.Parameters.AddWithValue("estado", cita.Estado ?? "pendiente");
                    cmd.Parameters.AddWithValue("motivo", cita.Motivo ?? "");
                    cmd.Parameters.AddWithValue("@fecha_registro", DateTime.Now);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        public List<CitaMedica> ListarCitas(int idMedico, DateTime fecha)
        {
            List<CitaMedica> lista = new List<CitaMedica>();

            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"
                    SELECT fecha
                    FROM citas
                    WHERE id_medico = @idMedico
                      AND DATE(fecha) = @fecha";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("idMedico", idMedico);
                    cmd.Parameters.AddWithValue("fecha", fecha.Date);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime fechaHora = reader.GetDateTime(0);
                            lista.Add(new CitaMedica
                            {
                                Fecha = fechaHora.Date,
                                Hora = fechaHora.TimeOfDay
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public List<DateTime> ObtenerHorariosDisponibles(int idMedico, DateTime fecha)
        {
            List<DateTime> horariosDisponibles = new List<DateTime>();
            string sql = @"
                SELECT hora 
                FROM citas 
                WHERE id_medico = @idMedico 
                AND fecha = @fecha 
                AND estado NOT IN ('pendiente', 'confirmada', 'realizada')
                UNION
                SELECT '09:00:00' AS hora -- Horario inicial
                UNION
                SELECT '09:30:00'
                UNION
                SELECT '10:00:00'
                UNION
                SELECT '10:30:00'
                UNION
                SELECT '11:00:00'
                UNION
                SELECT '11:30:00'
                UNION
                SELECT '14:00:00'
                UNION
                SELECT '14:30:00'
                UNION
                SELECT '15:00:00'
                UNION
                SELECT '15:30:00';";

                using (var con = conexion.Conectar()){
                con.Open();

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@idMedico", idMedico);
                    command.Parameters.AddWithValue("@fecha", fecha.Date);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["hora"] != DBNull.Value)
                            {
                                horariosDisponibles.Add(DateTime.Today.Add(reader.GetTimeSpan(0))); // Convierte hora a DateTime
                            }
                        }
                    }
                }
            }

            return horariosDisponibles;
        }


        public List<TimeSpan> ObtenerHorariosOcupados(int idMedico, DateTime fecha)
        {
            var  horariosOcupados = new List<TimeSpan>();
            string sql = @"
                SELECT hora 
                FROM citas 
                WHERE id_medico = @idMedico 
                AND fecha = @fecha 
                AND estado IN ('pendiente', 'confirmada', 'realizada');";
            using (var con = conexion.Conectar())
            {
                con.Open();
                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@idMedico", idMedico);
                    command.Parameters.AddWithValue("@fecha", fecha.Date);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["hora"] != DBNull.Value)
                            {
                                horariosOcupados.Add(reader.GetTimeSpan(0)); // Convierte hora a TimeSpan
                            }
                        }
                    }
                }
            }
            return horariosOcupados;
        }

        // Obtener las citas de un médico en un día específico
        public List<TimeSpan> ObtenerCitasPorFecha(int idMedico, DateTime fecha)
        {
            var citas = new List<TimeSpan>();
            string sql = @"SELECT hora FROM citas WHERE id_medico=@idMedico AND fecha=@fecha";

            using (var con = conexion.Conectar())
            {
                con.Open();
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@idMedico", idMedico);
                    cmd.Parameters.AddWithValue("@fecha", fecha.Date);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            citas.Add(reader.GetTimeSpan(0));
                        }
                    }
                }
            }

            return citas;
        }

        // Obtener turnos del médico para un día de la semana
        public List<(TimeSpan inicio, TimeSpan fin)> ObtenerTurnosMedico(int idMedico, int diaSemana)
        {
            var turnos = new List<(TimeSpan, TimeSpan)>();
            string sql = @"SELECT hora_inicio, hora_fin FROM horarios_medicos 
                       WHERE id_medico=@idMedico AND dia_semana=@diaSemana";

            using (var con = conexion.Conectar())
            {
                con.Open();
                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@idMedico", idMedico);
                    cmd.Parameters.AddWithValue("@diaSemana", diaSemana);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            turnos.Add((reader.GetTimeSpan(0), reader.GetTimeSpan(1)));
                        }
                    }
                }
            }

            return turnos;
        }





        public List<CitaMedica> ObtenerCitasPorPaciente(int idPaciente)
        {
            var lista = new List<CitaMedica>();

            using (var con = conexion.Conectar())
            {
                con.Open();
                    string query = @"SELECT c.id_cita, c.fecha, c.hora, c.estado, 
                                    COALESCE(m.nombre || ' ' || m.apellido, '') AS doctor
                                    FROM citas c
                                    LEFT JOIN medicos m ON c.id_medico = m.id_medico
                                    WHERE c.id_paciente = @id_paciente
                                    ORDER BY c.fecha DESC";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("id_paciente", idPaciente);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CitaMedica
                            {
                                IdCita = Convert.ToInt32(dr["id_cita"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Hora = TimeSpan.Parse(dr["hora"].ToString()),
                                Estado = dr["estado"].ToString(),
                               // Doctor = dr["doctor"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool ActualizarEstadoCita(int idCita, string nuevoEstado)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = "UPDATE citas SET estado = @estado WHERE id_cita = @id_cita";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("id_cita", idCita);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }







    }
}
