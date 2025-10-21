using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_ConsultasPacientes
    {
        private CD_ConsultasPacientes datos = new CD_ConsultasPacientes();

        
        public List<PacienteConsultaDTO> ObtenerPacientesRegistrados()
        {
            return datos.ListarPacientes();
        }
        
    }
}