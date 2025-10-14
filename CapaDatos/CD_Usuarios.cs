using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Configuration;
using Npgsql;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        private ConectionBD conexion = new ConectionBD();

        public Usuarios ValidarUser(string correo, string contrasena)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"SELECT id_usuario, nombre, apellido, correo, telefono, rol, esta_activo, fecha_actualizacion
                                FROM usuarios
                                WHERE correo=@correo AND contrasena=@contrasena AND esta_activo = TRUE";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("correo", correo);
                    cmd.Parameters.AddWithValue("contrasena", contrasena);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuarios
                            {
                                id_usuario = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                apellido = reader.GetString(2),
                                correo = reader.GetString(3),
                                telefono = reader.IsDBNull(4) ? null : reader.GetString(4),
                                rol = reader.GetString(5),
                                activo = reader.GetBoolean(6),
                                fecha_actualizacion = reader.GetDateTime(7)
                            };
                        }
                    }
                }

            }
            return null;
        }

        public void GuardarToken(int id_usuario, string token)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"INSERT INTO tokens_sesion (id_usuario, token, fecha_expiracion)
                                 VALUES (@id_usuario, @token, NOW() + INTERVAL '1 hour')";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("token", token);
                    cmd.Parameters.AddWithValue("id_usuario", id_usuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
