using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Npgsql;

namespace eSaludCareAdmin.Data
{
    public class ConectionBD
    {
        public static string StrConect = ConfigurationManager.ConnectionStrings["BDpsql"].ToString();

        /*ublic static NpgsqlConnection ObtenerConexion()
        {
            var conexion = new NpgsqlConnection(StrConect);
            conexion.Open();
            return conexion;

        }
        */
    }
}   