using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    [Table("citas", Schema = "public")]
    public class Citas
    {
        [Key]
        [Column("id_cita")]
        public int id_cita { get; set; }

        [Required]
        [Column("id_paciente")]
        public int id_paciente { get; set; }

        [Required]
        [Column("id_medico")]
        public int id_medico { get; set; }

        [Column("fecha")]
        public DateTime fecha { get; set; }

        [Column("hora")]
        public TimeSpan hora { get; set; }

        [Column("estado")]
        public string estado { get; set; }

        [Column("motivo")]
        public string motivo { get; set; }

        [Column("observaciones")]
        public string observaciones { get; set; }

        [Column("fecha_registro")]
        public DateTime fecha_registro { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
