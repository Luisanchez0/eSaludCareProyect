using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CapaEntidad
{
    public class Usuarios
    {
        /*

           -- TABLA: USUARIOS
       id_usuario SERIAL PRIMARY KEY,
       nombre VARCHAR(100) NOT NULL,
       apellido VARCHAR(100) NOT NULL,
       correo VARCHAR(150) UNIQUE NOT NULL,
       contrasena VARCHAR(255) NOT NULL,
       telefono VARCHAR(20),
       rol VARCHAR(20) CHECK(rol IN ('paciente', 'medico', 'admin')) NOT NULL,
       fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
   */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string telefono { get; set; }
        public string rol { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public string token { get; set; }
        public DateTime fecha_registro { get; set; }
    }

    public class UsuarioEntidad
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; } = "paciente";
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }


    public class PerfilesUser
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
    }

    public class UserCita { 
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
    }




}
