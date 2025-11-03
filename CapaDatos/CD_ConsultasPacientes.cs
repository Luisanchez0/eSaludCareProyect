using CapaEntidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_ConsultasPacientes
    {
        private readonly string _cadenaConexion;

        public CD_ConsultasPacientes(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public async Task<List<ConsultasPacientesDTO>> ObtenerPacientes()
        {
            var lista = new List<ConsultasPacientesDTO>();

            using (var conn = new NpgsqlConnection(_cadenaConexion))
            {
                await conn.OpenAsync();

                using (var cmd = new NpgsqlCommand(@"
                    SELECT 
                        pa.id_paciente,
                        us.nombre || ' ' || us.apellido AS nombre,
                        pa.fecha_nacimiento,
                        us.telefono,
                        pa.direccion,
                        us.correo
                    FROM pacientes pa
                    JOIN usuarios us ON pa.id_usuario = us.id_usuario", conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new ConsultasPacientesDTO
                            {
                                IdPaciente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                FechaNacimiento = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2),
                                Telefono = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Direccion = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Correo = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}