using CapaEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    [Table("pacientes", Schema = "public")]
    public class PacienteVista
    {

        [Key]
        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }

        public DateTime? fecha_nacimiento { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }

        // Relación con médico asignado
        [ForeignKey("IdMedico")]
        public virtual Medico MedicoAsignado { get; set; }
        public int IdMedico { get; set; }

        // Relación con usuario real
        public virtual Usuarios Usuario { get; set; }



    }
}