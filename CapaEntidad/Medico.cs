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


        public TimeSpan HoraInicio { get; set; } // Ej: 09:00
        public TimeSpan HoraFin { get; set; }   // Ej: 17:00
        public int DuracionTurnoMinutos { get; set; } = 30; // duración de cada cita
    }
}
