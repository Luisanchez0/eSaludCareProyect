using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace eSaludCareUsers.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext() : base("name=DefaultConnection") { }

        public DbSet<eSaludCareUsers.Models.Medico> Medicos { get; set;}
        public DbSet<eSaludCareUsers.Models.Paciente> Pacientes { get; set; }

        }
    }
