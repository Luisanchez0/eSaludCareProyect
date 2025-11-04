using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_ConsultasCitas
    {
        private readonly CD_ConsultasCitas datos;

        public CN_ConsultasCitas(string cadenaConexion)
        {
            datos = new CD_ConsultasCitas(cadenaConexion);
        }

        public List<CitaAgendadaDTO> ObtenerCitasAgendadas()
        {
            return datos.ListarCitas();
        }


    }
}