using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    [Table("usuarios", Schema = "public")]


    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellido { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        public string contrasena { get; set; }

        public string telefono { get; set; }

        [Required]
        public string rol { get; set; } // "paciente", "medico", "admin"

        public DateTime fecha_registro { get; set; }

        // Relaciones
        public virtual Paciente Paciente { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual ICollection<TokenSesion> TokensSesion { get; set; }

    }
}