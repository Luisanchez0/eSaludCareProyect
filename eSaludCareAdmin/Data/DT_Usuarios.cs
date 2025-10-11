using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using eSaludCareAdmin.Models;
using Npgsql;

namespace eSaludCareAdmin.Data
{
    public class DT_Usuarios
    {
        public List<Usuarios> listar()
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {

                using(SqlConnection con = new SqlConnection(ConectionBD.StrConect))
                {
                    string Query = "SELECT id_usuario, nombre, apellido, correo, contrasena, telefono, rol, fecha_registro FROM usuarios";
                    SqlCommand cmd = new SqlCommand(Query, con);

                    cmd.CommandType = System.Data.CommandType.Text;
                    con.Open();
                
                
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Usuarios()
                                {
                                    IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                    Nombre = dr["nombre"].ToString(),
                                    Apellido = dr["apellido"].ToString(),
                                    Correo = dr["correo"].ToString(),
                                    Contrasena = dr["contrasena"].ToString(),
                                    Telefono = dr["telefono"].ToString(),
                                    Rol = dr["rol"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["fecha_registro"])
                                }
                            );
                        }
                    } 
                }

            }
            catch (Exception)
            {
                lista = new List<Usuarios>();
            }
            return lista;
        }
    }
}