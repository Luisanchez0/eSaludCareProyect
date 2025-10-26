using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using eSaludCareUsers.Models;


namespace CapaComun
{
    public static class MedicoMapper
    {
        public static eSaludCareUsers.Models.MedicoVista AModelo(MedicoDTO dto)
        {
            return new eSaludCareUsers.Models.MedicoVista

            {
                id_usuario = dto.IdUsuario,
                especialidad = dto.Especialidad,
                numero_cedula = dto.NumeroCedula
            };
        }
    }

}
