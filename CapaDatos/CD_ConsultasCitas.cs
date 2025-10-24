using System.Collections.Generic;
using Npgsql;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConsultasCitas
    {
        private ConectionBD conexion = new ConectionBD();

        public List<CitaAgendadaDTO> ListarCitas()
        {
            var lista = new List<CitaAgendadaDTO>();

            using (var con = conexion.Conectar())
            {
                var cmd = new NpgsqlCommand(@"
                    SELECT c.id_cita, 
                           p.nombre AS paciente, 
                           m.nombre AS medico, 
                           c.fecha, 
                           c.hora, 
                           e.nombre AS especialidad, 
                           c.estado
                    FROM citas c
                    JOIN usuarios p ON c.id_paciente = p.id_usuario
                    JOIN usuarios m ON c.id_medico = m.id_usuario
                    JOIN especialidades e ON c.id_especialidad = e.id_especialidad", con);

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

            return lista;
        }
    }
}