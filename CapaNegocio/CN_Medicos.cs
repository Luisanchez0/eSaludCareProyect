using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Medicos
    {
        private CD_Medicos objetoCD = new CapaDatos.CD_Medicos();
        public List<MedicoAsignado> ListarMedicos()
        {
            return objetoCD.ListarMedicos();
        }
    }
}
