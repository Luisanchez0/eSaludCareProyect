using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public static class MedicoMapper
    {
        public static Medico AModelo(MedicoDTO dto)
        {
            return new Medico
            {
                id_usuario = dto.IdUsuario,
                especialidad = dto.Especialidad,
                numero_cedula = dto.NumeroCedula
            };
        }
    }
}
