using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Tokens
    {
        private ConectionBD conexion = new ConectionBD();

        public int ObtenerIdUsuarioPorToken (string token)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();

                string query = @"SELECT id_usuario 
                                 FROM tokens_sesion 
                                 WHERE token = @token 
                                   AND (fecha_expiracion IS NULL OR fecha_expiracion > NOW())";

                using (var cmd = new Npgsql.NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@token", token);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }

            }

        }

    }
}
