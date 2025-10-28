using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSaludCareUsers.Models
{
    [Table("CitaMedica", Schema = "public")]
    public class CitaMedica
    {
        [Key]
        [Column("id_cita")]
        public int IdCita { get; set; }

        [ForeignKey("Medico")]
        [Column("id_medico")]
        public int IdMedico { get; set; }

        [ForeignKey("Paciente")]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; } // Solo la fecha

        [Column("hora")]
        public TimeSpan Hora { get; set; } // ✅ mejor usar TimeSpan si la columna es TIME en PostgreSQL

        [Column("motivo")]
        [MaxLength(500)]
        public string Motivo { get; set; }

        [Column("estado")]
        [MaxLength(20)]
        public string Estado { get; set; } = "pendiente"; // pendiente / confirmada / cancelada / realizada

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relaciones
        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
