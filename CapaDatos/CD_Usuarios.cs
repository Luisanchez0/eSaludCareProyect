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
        //registro de usuarios
        public bool InsertarUsuario(UsuarioEntidad usuario)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"INSERT INTO usuarios (nombre, apellido, correo, contrasena, telefono, rol, fecha_registro, esta_activo)
                                 VALUES (@nombre, @apellido, @correo, @contrasena, @telefono, @rol, @fecha_registro, TRUE)";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("telefono", usuario.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("fecha_registro", usuario.FechaRegistro);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }
        public List<Usuarios> ObtenerUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {
                using (var conexion = new NpgsqlConnection("Host = localhost; Port = 5432; Username = postgres; Password = 102538; Database = clinica_db"))
                {
                    conexion.Open();
                    using (var comando = new NpgsqlCommand("SELECT * FROM usuarios", conexion))
                    {
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Usuarios
                                {
                                    id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                    nombre = reader["nombre"].ToString(),
                                    correo = reader["correo"].ToString(),
                                    contrasena = reader["contrasena"].ToString(),
                                    rol = reader["rol"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes loguear el error si quieres
                // Pero no necesitas devolver aquí, porque el return está abajo
            }

            return lista; // ✅ Este return está fuera del try-catch y siempre se ejecuta
        }
        public bool EliminarUsuario(int id)
        {
            bool resultado = false;

            try
            {
                using (var conexion = new NpgsqlConnection("Host = localhost; Port = 5432; Username = postgres; Password = 102538; Database = clinica_db"))
                {
                    conexion.Open();
                    using (var comando = new NpgsqlCommand("DELETE FROM usuarios WHERE id_usuario = @id", conexion))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        int filasAfectadas = comando.ExecuteNonQuery();
                        resultado = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes loguear el error si quieres
                resultado = false;
            }

            return resultado; // ✅ Este return siempre se ejecuta
        }

        public bool ActualizarUsuario(Usuarios usuario)
        {
            bool resultado = false;

            try
            {
                using (var conexion = new NpgsqlConnection("Host = localhost; Port = 5432; Username = postgres; Password = 102538; Database = clinica_db"))
                {
                    conexion.Open();
                    using (var comando = new NpgsqlCommand(@"
                UPDATE usuarios 
                SET nombre = @nombre, correo = @correo, contrasena = @contrasena, rol = @rol 
                WHERE id_usuario = @id", conexion))
                    {
                        comando.Parameters.AddWithValue("@id", usuario.id_usuario);
                        comando.Parameters.AddWithValue("@nombre", usuario.nombre);
                        comando.Parameters.AddWithValue("@correo", usuario.correo);
                        comando.Parameters.AddWithValue("@contrasena", usuario.contrasena);
                        comando.Parameters.AddWithValue("@rol", usuario.rol);

                        int filasAfectadas = comando.ExecuteNonQuery();
                        resultado = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }
    }
}
