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

        public List<Usuarios> listar()
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {

                using (NpgsqlConnection con = new NpgsqlConnection(ConectionBD.StrConect))
                {
                    string Query = "SELECT id_usuario, nombre, apellido, correo, contrasena, telefono, fecha_registro FROM usuarios";
                    NpgsqlCommand cmd = new NpgsqlCommand(Query, con);

                    cmd.CommandType = System.Data.CommandType.Text;
                    con.Open();


                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
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
