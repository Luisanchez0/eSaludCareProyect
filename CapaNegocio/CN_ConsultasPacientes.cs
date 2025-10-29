using CapaEntidad;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CN_ConsultasPacientes
{
    private readonly CD_ConsultasPacientes accesoDatos;

    public CN_ConsultasPacientes(string cadenaConexion)
    {
        accesoDatos = new CD_ConsultasPacientes(cadenaConexion);
    }

    public async Task<List<ConsultaPacientesDTO>> ObtenerPacientes()
    {
        var pacientes = await accesoDatos.ConsultaPacientes();

        // Aquí podrías aplicar lógica adicional si fuera necesario
        return pacientes;
    }
}
