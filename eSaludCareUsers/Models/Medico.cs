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
        [ForeignKey("Usuario")] 
        public int id_usuario { get; set; }

        public string especialidad { get; set; }
        public string numero_cedula { get; set; }

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