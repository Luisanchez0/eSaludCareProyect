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
        private static string StrConect = ConfigurationManager.ConnectionStrings["BDpsql"].ConnectionString;

        public static NpgsqlConnection ObtenerConexion()
        {
            var conexion = new NpgsqlConnection(StrConect);
            conexion.Open();
            return conexion;

        }
    }
}   