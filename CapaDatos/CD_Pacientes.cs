using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using Npgsql;

namespace CapaDatos
{
    public class CD_Pacientes
    {
        private ConectionBD conexion = new ConectionBD();

        public PerfilPaciente ObtenerPacientePorUsuario(int idUsuario)
        {
            PerfilPaciente perfil = null;
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"SELECT u.telefono, p.fecha_nacimiento, p.genero, p.direccion 
                                 FROM usuarios u
                                 INNER JOIN pacientes p ON u.id_usuario = p.id_usuario
                                 WHERE u.id_usuario = @id";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            perfil = new PerfilPaciente
                            {
                                IdUsuario = idUsuario,
                                Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : null,
                                FechaNacimiento = reader["fecha_nacimiento"] != DBNull.Value ? (DateTime?)reader["fecha_nacimiento"] : null,
                                Genero = reader["genero"] != DBNull.Value ? reader["genero"].ToString() : null,
                                Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : null
                            };
                        }
                    }

                    return perfil;


                }

            }

        }


        public bool ActualizarPerfilPaciente(PerfilPaciente perfil)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"UPDATE usuarios SET telefono = @telefono WHERE id_usuario = @id;
                                 UPDATE pacientes SET fecha_nacimiento = @fecha_nacimiento, genero = @genero, direccion = @direccion 
                                 WHERE id_usuario = @id;";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@telefono", (object)perfil.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", (object)perfil.FechaNacimiento ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@genero", (object)perfil.Genero ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@direccion", (object)perfil.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", perfil.IdUsuario);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
