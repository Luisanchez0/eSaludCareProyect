using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad

{
    public class ConsultasPacientesDTO
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; } // ✅ permite null
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
    }
}



