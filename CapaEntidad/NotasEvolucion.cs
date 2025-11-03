using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class NotasEvolucion
    {
        public int id_nota { get; set; }
        public int id_historial { get; set; }
        public int id_medico { get; set; }
        public string diagnostico { get; set; }
        public string signos_vitales { get; set; }
        public string resultados_estudios { get; set; }
        public string tratamiento_actual { get; set; }
        public string pronostico { get; set; }
        public string observaciones { get; set; }
        public DateTime fecha_nota { get; set; }
    }
}
