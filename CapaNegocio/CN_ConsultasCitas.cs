using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_ConsultasCitas
    {
        private CD_ConsultasCitas datos = new CD_ConsultasCitas();

        public List<CitaAgendadaDTO> ObtenerCitasAgendadas()
        {
            return datos.ListarCitas();
        }
    }
}