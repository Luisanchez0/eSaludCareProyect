using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    public class TokenSesion
    {
        [Key]
        public int id_token { get; set; }

        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }

        public string token { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime? fecha_expiracion { get; set; }

        public virtual UsuarioEntidad Usuario { get; set; }

    }
}