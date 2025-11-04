using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public int IdUsuario { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }
    }
}
