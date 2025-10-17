using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace eSaludCareUsers.Data
{
    public class NpgsqlConfiguration : DbConfiguration
    {
        public NpgsqlConfiguration()
        {
            SetProviderServices("Npgsql", NpgsqlServices.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
        }

    }
}