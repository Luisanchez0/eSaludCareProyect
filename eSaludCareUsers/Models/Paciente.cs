using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    [Table("pacientes", Schema = "public")]
    public class Paciente
    {

        [Key]
        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }

        public DateTime? fecha_nacimiento { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }
        public int IdMedico { get; set; }

        public virtual UsuarioEntidad Usuario { get; set; }
        public virtual Medico MedicoAsignado { get; set; }


    }
}