using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class UsuarioMedicoDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
        public string Contraseña { get; set; }

        // Solo si el rol es médico
        public string Especialidad { get; set; }
        public string NumeroCedula { get; set; }

    }
}
