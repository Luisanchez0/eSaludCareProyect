using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class HistorialClinico
    {
        public int id_historial { get; set; }
        public int id_paciente { get; set; }
        public int id_medico { get; set; }
        public int? id_cita { get; set; }

        public string motivo_consulta { get; set; }
        public string padecimiento_actual { get; set; }
        public string antecedentes_personales { get; set; }
        public string antecedentes_familiares { get; set; }

        public decimal? peso { get; set; }
        public decimal? talla { get; set; }
        public string presion_arterial { get; set; }
        public int? frecuencia_cardiaca { get; set; }
        public int? frecuencia_respiratoria { get; set; }
        public decimal? temperatura { get; set; }
        public string exploracion_general { get; set; }

        public string diagnostico { get; set; }
        public string pronostico { get; set; }
        public string tratamiento { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime fecha_actualizacion { get; set; }




    }
}
