using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConsultasPacientes
    {
        private ConectionBD conexion = new ConectionBD();

        public List<PacienteConsultaDTO> ListarPacientes()
        {
            var lista = new List<PacienteConsultaDTO>();

            using (var con = conexion.Conectar())
            {
                var cmd = new NpgsqlCommand(@"
                    SELECT p.id_paciente, u.nombre, u.correo, u.telefono, 
                           p.fecha_nacimiento, p.genero, p.direccion
                    FROM pacientes p
                    JOIN usuarios u ON p.id_usuario = u.id_usuario
                    WHERE u.rol = 'paciente'", con);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new PacienteConsultaDTO
                        {
                            IdPaciente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Correo = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            FechaNacimiento = reader.GetDateTime(4),
                            Genero = reader.GetString(5),
                            Direccion = reader.GetString(6)
                        });
                    }
                }
            }

            return lista;
        }
    }
}