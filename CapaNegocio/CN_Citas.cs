using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Citas
    {
        private CD_Citas _citaDatos = new CD_Citas();
        public bool RegistrarCita(CapaEntidad.CitaMedica cita)
        {
            return _citaDatos.RegistrarCita(cita);
        }
    }
}
