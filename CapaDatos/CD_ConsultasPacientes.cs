using CapaEntidad;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CD_ConsultasPacientes
{
    private readonly string cadenaConexion;

    public CD_ConsultasPacientes(string cadenaConexion)
    {
        this.cadenaConexion = cadenaConexion;
    }

    public async Task<List<ConsultaPacientesDTO>> ConsultaPacientes()
    {
        using (var connection = new NpgsqlConnection(cadenaConexion))
        {
            await connection.OpenAsync();

            var resultado = await connection.QueryAsync<ConsultaPacientesDTO>(
                "SELECT * FROM ConsultaPaciente()"
            );

            return resultado.AsList();
        }
    }
}
