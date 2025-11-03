using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_HistorialClinico
    {
        private readonly CD_HistorialClinico _datos = new CD_HistorialClinico();

        public bool Guardar(HistorialClinico h)
        {
            return _datos.Guardar(h);
        }

        public List<HistorialClinico> ListarPorPaciente(int id_paciente)
        {
            return _datos.ListarPorPaciente(id_paciente);
        }
    }
}
