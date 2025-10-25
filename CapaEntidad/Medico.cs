using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public int IdUsuario { get; set; }
        public string Especialidad { get; set; }
        public string CedulaProfesional { get; set; }
    }
}
