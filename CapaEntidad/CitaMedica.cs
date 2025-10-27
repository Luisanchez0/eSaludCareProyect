using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CitaMedica
    {
        /*
        id_cita SERIAL PRIMARY KEY,
        id_paciente INT NOT NULL REFERENCES pacientes(id_paciente) ON DELETE CASCADE,
        id_medico INT NOT NULL REFERENCES medicos(id_medico) ON DELETE CASCADE,
        fecha_cita TIMESTAMP NOT NULL,
        estado VARCHAR(20) CHECK(estado IN ('pendiente', 'confirmada', 'cancelada', 'realizada')) DEFAULT 'pendiente',
        motivo TEXT,
        observaciones TEXT,
        fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP*/

        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        public string Estado { get; set; } // "Pendiente", "Confirmada", "Cancelada"
        public string Motivo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string doctor { get; set; }
    }
}
