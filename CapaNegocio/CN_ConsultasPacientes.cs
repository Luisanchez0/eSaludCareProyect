using CapaEntidad;
using CapaDatos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_ConsultasPacientes
    {
        private readonly CD_ConsultasPacientes accesoDatos;

        public CN_ConsultasPacientes(string cadenaConexion)
        {
            accesoDatos = new CD_ConsultasPacientes(cadenaConexion);
        }

        public async Task<List<ConsultasPacientesDTO>> ObtenerPacientes()
        {
            var pacientes = await accesoDatos.ObtenerPacientes();

            // Aquí podrías aplicar lógica adicional si fuera necesario
            return pacientes;
        }
    }
}
