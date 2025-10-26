using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    [Table("medico", Schema = "public")]

    public class Medico
    {
        [Key]
        public int id_medico { get; set; }

        [Required]
        public int id_usuario { get; set; }

        [Required]
        public string especialidad { get; set; }

        [Required]
        public string numero_cedula { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuarios Usuario { get; set; }

    }
}
