using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_HistorialMedico
    {
        private readonly CD_HistorialMedico objetoCD = new CD_HistorialMedico();

        public List<HistorialMedico> ListarPorPaciente(int idPaciente)
        {
            return objetoCD.ListarPorPaciente(idPaciente);
        }

        public bool Guardar(HistorialMedico historial)
        {
            return objetoCD.Guardar(historial);
        }

        public bool Actualizar(HistorialMedico historial)
        {
            return objetoCD.Actualizar(historial);
        }
    }
}
