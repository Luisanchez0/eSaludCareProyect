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

        public List<CitaAgendadaDTO> ListarCitas()
        {
            var lista = new List<CitaAgendadaDTO>();

            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@"
                    SELECT 
                        c.id_cita,
                        u_p.nombre || ' ' || u_p.apellido AS NombrePaciente,
                        u_m.nombre || ' ' || u_m.apellido AS NombreMedico,
                        c.fecha,
                        TO_CHAR(c.hora, 'HH24:MI') AS Hora,
                        m.especialidad,
                        c.estado
                    FROM citas c
                    JOIN pacientes p ON c.id_paciente = p.id_paciente
                    JOIN usuarios u_p ON p.id_usuario = u_p.id_usuario
                    JOIN medicos m ON c.id_medico = m.id_medico
                    JOIN usuarios u_m ON m.id_usuario = u_m.id_usuario", conn))
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
    }
}