using eSaludCareUsers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public DbSet<eSaludCareUsers.Models.Citas> Citas { get; set; }

        //agregados recientemente



        public DbSet<UsuarioEntidad> Usuarios { get; set; }
     


       public DbSet<CitaMedica> CitaMedica { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            // ✅ Relación 1 → N (aunque en BD es 1:1 por el UNIQUE)
            modelBuilder.Entity<UsuarioEntidad>()
                .HasMany(u => u.Medicos)
                .WithRequired(m => m.Usuario)
                .HasForeignKey(m => m.id_usuario);

            base.OnModelCreating(modelBuilder);
        }
    }
}
