using System;
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

            using (var conexion = new NpgsqlConnection(_cadenaConexion))
            {
                conexion.Open();

                string query = @"
                    SELECT 
                        c.id_cita,
                        c.id_paciente,
                        u.nombre AS nombre_paciente,
                        c.id_medico,
                        mu.nombre AS nombre_medico,
                        c.fecha,
                        c.hora,
                        c.estado
                    FROM public.citas c
                    INNER JOIN public.pacientes p ON c.id_paciente = p.id_paciente
                    INNER JOIN public.usuarios u ON p.id_usuario = u.id_usuario
                    INNER JOIN public.medicos m ON c.id_medico = m.id_medico
                    INNER JOIN public.usuarios mu ON m.id_usuario = mu.id_usuario
                    ORDER BY c.fecha, c.hora;
                ";

                using (var comando = new NpgsqlCommand(query, conexion))
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new CitaAgendadaDTO
                        {
                            IdCita = reader.GetInt32(reader.GetOrdinal("id_cita")),
                            IdPaciente = reader.GetInt32(reader.GetOrdinal("id_paciente")),
                            NombrePaciente = reader.GetString(reader.GetOrdinal("nombre_paciente")),
                            IdMedico = reader.GetInt32(reader.GetOrdinal("id_medico")),
                            NombreMedico = reader.GetString(reader.GetOrdinal("nombre_medico")),
                            Fecha = reader.IsDBNull(reader.GetOrdinal("fecha")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("fecha")),
                            Hora = reader.IsDBNull(reader.GetOrdinal("hora")) ? (TimeSpan?)null : reader.GetTimeSpan(reader.GetOrdinal("hora")),
                            Estado = reader.GetString(reader.GetOrdinal("estado"))
                        });
                    }
                }
            }

            return lista;
        }
    }
}