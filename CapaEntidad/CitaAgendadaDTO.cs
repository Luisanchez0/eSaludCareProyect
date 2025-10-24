using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CitaAgendadaDTO
    {
        public int IdCita { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreMedico { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Especialidad { get; set; }
        public string Estado { get; set; }


    }
}
