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

        public Usuarios ObtenerUsuarioPorCorreo(string correo)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = "SELECT * FROM usuarios WHERE correo = @correo";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@correo", correo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuarios
                            {
                                id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                nombre = reader["nombre"].ToString(),
                                correo = reader["correo"].ToString(),
                                contrasena = reader["contrasena"].ToString(),
                                rol = reader["rol"].ToString()
                            };
                        }
                    }
                }
                return null;

            }
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

                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        string query = @"INSERT INTO usuarios (nombre, apellido, correo, contrasena, telefono, rol, fecha_registro, esta_activo)
                                 VALUES (@nombre, @apellido, @correo, @contrasena, @telefono, @rol, @fecha_registro, TRUE)
                                 RETURNING id_usuario;";

                        int idUsuario;

                        using (var cmd = new NpgsqlCommand(query, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
                            cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
                            cmd.Parameters.AddWithValue("correo", usuario.Correo);
                            cmd.Parameters.AddWithValue("contrasena", usuario.Contrasena);
                            cmd.Parameters.AddWithValue("telefono", usuario.Telefono ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("rol", usuario.Rol);
                            cmd.Parameters.AddWithValue("fecha_registro", usuario.FechaRegistro);

                            idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        if (usuario.Rol.ToLower() == "paciente")
                        {
                            string query2 = @"INSERT INTO pacientes (id_usuario)
                                    VALUES (@idUsuario)";

                            using (var cmd2 = new NpgsqlCommand(query2, con, transaction))
                            {
                                cmd2.Parameters.AddWithValue(@"idUsuario", idUsuario);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        else if (usuario.Rol.ToLower() == "medico")
                        {
                            string query3 = @"INSERT INTO medicos (id_usuario, especialidad, numero_cedula)
                                    VALUES (@idUsuario, @especialidad, @numero_cedula)";
                            using (var cmd3 = new NpgsqlCommand(query3, con, transaction))
                            {
                                cmd3.Parameters.AddWithValue(@"idUsuario", idUsuario);
                                cmd3.Parameters.AddWithValue(@"especialidad", usuario.Especialidad ?? (object)DBNull.Value);
                                cmd3.Parameters.AddWithValue(@"numero_cedula", usuario.NumeroCedula ?? (object)DBNull.Value);
                                cmd3.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public List<Usuarios> ObtenerUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {
                using (var con = conexion.Conectar())
                {
                    con.Open();
                    using (var comando = new NpgsqlCommand("SELECT * FROM usuarios", con))
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
                using (var con = conexion.Conectar())
                {
                    con.Open();
                    using (var comando = new NpgsqlCommand(@"
                UPDATE usuarios 
                SET nombre = @nombre, correo = @correo, contrasena = @contrasena, rol = @rol 
                WHERE id_usuario = @id", con))
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

        public UsuarioEntidad ObtenerUsuarioPorId(int idUsuario)
        {
            UsuarioEntidad usuario = null;
            try
            {
                using (var con = conexion.Conectar())
                {
                    con.Open();
                    string query = @"SELECT id_usuario, nombre, apellido, correo, telefono, rol, esta_activo, fecha_registro, fecha_actualizacion
                                 FROM usuarios
                                 WHERE id_usuario = @id";
                    using (var cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", idUsuario);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new UsuarioEntidad
                                {
                                    IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Apellido = reader["apellido"].ToString(),
                                    Correo = reader["correo"].ToString(),
                                    Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : null,
                                    Rol = reader["rol"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(reader["fecha_registro"]),
                                    fecha_actualizacion = Convert.ToDateTime(reader["fecha_actualizacion"])
                                };
                            }
                        }

                        return usuario;


                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores si es necesario
            }
            return usuario;
        }

        public int ObtenerIdPorCorreo(string correo)
        {
            try
            {
                using (var conn = conexion.Conectar())
                {
                    conn.Open();
                    string query = "SELECT id_usuario FROM usuarios WHERE correo = @correo AND esta_activo = TRUE";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@correo", correo);
                        var result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }


        public bool ActualizarClaveTemporal(int idUsuario, string nuevaClave)
        {
            try
            {
                using (var conn = conexion.Conectar())
                {
                    conn.Open();
                    string query = "UPDATE usuarios SET contrasena = @clave, fecha_actualizacion = CURRENT_TIMESTAMP WHERE id_usuario = @id";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clave", nuevaClave);
                        cmd.Parameters.AddWithValue("@id", idUsuario);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool CambiarContrasena(int idUsuario, string nuevaClave)
        {
            try
            {
                using (var conn = conexion.Conectar())
                {
                    conn.Open();
                    string query = "UPDATE usuarios SET contrasena = @clave, fecha_actualizacion = CURRENT_TIMESTAMP WHERE id_usuario = @id";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clave", nuevaClave);
                        cmd.Parameters.AddWithValue("@id", idUsuario);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar contraseña: " + ex.Message);
                return false;
            }
        }




    }
}
