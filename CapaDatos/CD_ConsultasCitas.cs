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
            List<CitaAgendadaDTO> lista = new List<CitaAgendadaDTO>();

            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@"
                    SELECT 
                        ci.id_cita,
                        us.nombre AS nombre_paciente,
                        med.nombre AS nombre_medico,
                        ci.fecha_cita,
                        TO_CHAR(ci.fecha_cita, 'HH24:MI') AS hora,
                        me.especialidad,
                        ci.estado
                    FROM citas ci
                    JOIN pacientes pa ON ci.id_paciente = pa.id_paciente
                    JOIN usuarios us ON pa.id_usuario = us.id_usuario
                    JOIN medicos me ON ci.id_medico = me.id_medico
                    JOIN usuarios med ON me.id_usuario = med.id_usuario", conn))
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