using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    [Table("medicos", Schema = "public")]
    public class Medico
    {
        [Key]
        [Column("id_medico")]
        public int id_medico { get; set; }

        [Required]
        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Required]
        [Column("especialidad")]
        public string especialidad { get; set; }

        [Required]
        [Column("numero_cedula")]
        public string numero_cedula { get; set; }

        public TimeSpan? hora_inicio { get; set; }

        public TimeSpan? hora_fin { get; set; }

        public int duracion_turno_minutos { get; set; } = 30;

        [ForeignKey("id_usuario")]
        public virtual UsuarioEntidad Usuario { get; set; }
    }
    public class MedicoDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Especialidad { get; set; }
        public string NumeroCedula { get; set; }
    }

}