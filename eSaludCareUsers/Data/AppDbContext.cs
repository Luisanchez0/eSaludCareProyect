using eSaludCareUsers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;


namespace eSaludCareUsers.Data
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]

    public class AppDbContext: DbContext
    {
        public AppDbContext() : base("name=BDpsql") { }

        public DbSet<eSaludCareUsers.Models.Medico> Medicos { get; set; }
        public DbSet<eSaludCareUsers.Models.Paciente> Pacientes { get; set; }
        public DbSet<UsuarioEntidad> Usuarios { get; set; }
     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public"); // 👈 Esto es clave para PostgreSQL
            base.OnModelCreating(modelBuilder);
        }

    }
}
