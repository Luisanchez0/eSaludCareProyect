using System.Collections.Generic;
using Npgsql;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConsultasCitas
    {
        private readonly string _cadenaConexion;

        public CD_ConsultasCitas(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        // Listar todas las citas agendadas
        public List<CitaAgendadaDTO> ListarCitas()
        {
            var lista = new List<CitaAgendadaDTO>();

            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@"
                    SELECT 
                        c.id_cita_agendada,
                        p.nombre AS nombre_paciente,
                        m.nombre AS nombre_medico,
                        c.fecha,
                        c.hora,
                        e.nombre AS especialidad,
                        c.estado
                    FROM citas_agendadas c
                    JOIN pacientes p ON c.id_paciente = p.id_paciente
                    JOIN medicos m ON c.id_medico = m.id_medico
                    JOIN especialidades e ON c.id_especialidad = e.id_especialidad", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CitaAgendadaDTO
                            {
                                IdCita = reader.GetInt32(0),
                                NombrePaciente = reader.GetString(1),
                                NombreMedico = reader.GetString(2),
                                Fecha = reader.GetDateTime(3),
                                Hora = reader.GetString(4),
                                Especialidad = reader.GetString(5),
                                Estado = reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // Buscar cita por ID
        public CitaAgendadaDTO BuscarCitaPorId(int id)
        {
            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@"
                    SELECT 
                        c.id_cita_agendada,
                        p.nombre AS nombre_paciente,
                        m.nombre AS nombre_medico,
                        c.fecha,
                        c.hora,
                        e.nombre AS especialidad,
                        c.estado
                    FROM citas_agendadas c
                    JOIN pacientes p ON c.id_paciente = p.id_paciente
                    JOIN medicos m ON c.id_medico = m.id_medico
                    JOIN especialidades e ON c.id_especialidad = e.id_especialidad
                    WHERE c.id_cita_agendada = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CitaAgendadaDTO
                            {
                                IdCita = reader.GetInt32(0),
                                NombrePaciente = reader.GetString(1),
                                NombreMedico = reader.GetString(2),
                                Fecha = reader.GetDateTime(3),
                                Hora = reader.GetString(4),
                                Especialidad = reader.GetString(5),
                                Estado = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Insertar nueva cita
        public bool InsertarCita(CitaAgendadaDTO cita)
        {
            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@"
                    INSERT INTO citas_agendadas (id_paciente, id_medico, fecha, hora, id_especialidad, estado)
                    VALUES (@idPaciente, @idMedico, @fecha, @hora, @idEspecialidad, @estado)", conn))
                {
                    cmd.Parameters.AddWithValue("@idPaciente", cita.IdCita); // Ajustar si tienes ID real
                    cmd.Parameters.AddWithValue("@idMedico", cita.NombreMedico); // Reemplazar con ID real
                    cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("@hora", cita.Hora);
                    cmd.Parameters.AddWithValue("@idEspecialidad", cita.Especialidad); // Reemplazar con ID real
                    cmd.Parameters.AddWithValue("@estado", cita.Estado);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Actualizar cita existente
        public bool ActualizarCita(int id, CitaAgendadaDTO cita)
        {
            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@"
                    UPDATE citas_agendadas
                    SET fecha = @fecha, hora = @hora, estado = @estado
                    WHERE id_cita_agendada = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("@hora", cita.Hora);
                    cmd.Parameters.AddWithValue("@estado", cita.Estado);
                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Eliminar cita
        public bool EliminarCita(int id)
        {
            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM citas_agendadas WHERE id_cita_agendada = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}