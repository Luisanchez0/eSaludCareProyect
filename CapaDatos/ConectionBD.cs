using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace CapaDatos
{
    public class ConectionBD
    {
        public static string StrConect = ConfigurationManager.ConnectionStrings["BDpsql"].ConnectionString;

        public NpgsqlConnection Conectar()
        {
            return new NpgsqlConnection(StrConect);
        }

    }
}
