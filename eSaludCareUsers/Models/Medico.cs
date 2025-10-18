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
}