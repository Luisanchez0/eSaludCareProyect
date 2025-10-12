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

        public Usuarios Login(string correo, string contraseña)
        {
            Usuarios usuario = null;

                using (var con = new NpgsqlConnection(ConectionBD.StrConect))
                {
                    con.Open();
                    var Query = @"SELECT id_usuario, nombre, apellido, correo, telefono, rol, fecha_registro 
                            FROM usuarios 
                            WHERE correo = @correo AND contrasena = @contrasena AND esta_activo = true";

                using (var cmd = new NpgsqlCommand(Query, con))
                    {
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contrasena", contraseña);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                            usuario = new Usuarios()
                                {
                                    IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Apellido = reader["apellido"].ToString(),
                                    Correo = reader["correo"].ToString(),
                                    Telefono = reader["telefono"].ToString(),
                                    Rol = reader["rol"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(reader["fecha_registro"])

                            };
                            }
                        }
                    }
                }

            return usuario;



        }

    }
}
