using System.Collections.Generic;
using Npgsql;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConsultasPacientes
    {
        private ConectionBD conexion = new ConectionBD();

        public List<ConsultaPacientesDTO> ListarPacientes()
        {
        var lista = new List<ConsultaPacientesDTO>();

            using (var con = conexion.Conectar())
            {
                con.Open(); // ✅ ESTA LÍNEA ES CLAVE

                var query = @"
                    SELECT p.id_paciente, u.nombre, u.apellido, p.fecha_nacimiento, u.genero, u.direccion
                    FROM pacientes p
                    JOIN usuarios u ON p.id_usuario = u.id_usuario
                    JOIN usuarios_roles ur ON u.id_usuario = ur.id_usuario
                    WHERE ur.id_rol = 3"; // ← Ajusta según tu lógica de roles

                using (var cmd = new NpgsqlCommand(query, con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ConsultaPacientesDTO
                        {
                            IdPaciente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Correo = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            FechaNacimiento = reader.GetDateTime(4),
                            Direccion = reader.GetString(6)
                        });
                    }
                }
            }

            return lista;
        }
    }
}
