using System;
using System.Collections.Generic;
using Npgsql;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConsultasCitas
    {
        private readonly string cadenaConexion;

        public CD_ConsultasCitas(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public List<CitaAgendadaDTO> ListarCitas()
        {
            List<CitaAgendadaDTO> lista = new List<CitaAgendadaDTO>();

            using (var conn = new NpgsqlConnection(cadenaConexion))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"SELECT 
                        c.id_cita,
                        c.id_paciente,
                        p.nombre AS nombre_paciente,
                        c.id_medico,
                        m.nombre AS nombre_medico,
                        c.fecha,
                        c.hora
                    FROM citas c
                    INNER JOIN pacientes p ON c.id_paciente = p.id_paciente
                    INNER JOIN medicos m ON c.id_medico = m.id_medico
                    ORDER BY c.fecha, c.hora", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CitaAgendadaDTO
                            {
                                IdCita = reader.GetInt32(0),
                                IdPaciente = reader.GetInt32(1),
                                NombrePaciente = reader.GetString(2),
                                IdMedico = reader.GetInt32(3),
                                NombreMedico = reader.GetString(4),
                                Fecha = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                Hora = reader.IsDBNull(6) ? TimeSpan.Zero : reader.GetTimeSpan(6)
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}