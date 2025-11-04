using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class HistorialMedico
    {
        public int id_historial_medico { get; set; }
        public int id_paciente { get; set; }
        public string enfermedades_previas { get; set; }
        public string alergias { get; set; }
        public string cirugias { get; set; }
        public string medicamentos_actuales { get; set; }
        public string antecedentes_familiares { get; set; }
        public string habitos { get; set; }
        public string observaciones_medico { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}
