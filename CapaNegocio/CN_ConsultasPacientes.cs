using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ConsultasPacientes
    {
        private CD_ConsultasPacientes datos = new CD_ConsultasPacientes();

        public List<ConsultaPacientesDTO> ObtenerPacientesRegistrados()
        {
            return datos.ListarPacientes();
        }


    }
}
