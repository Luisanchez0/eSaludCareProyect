using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Medicos
    {

        private ConectionBD conexion = new ConectionBD();

        public List<Medico> ListarMedicos()
        {
            List<Medico> lista = new List<Medico>();
            try
            {
                using (var con = conexion.Conectar())
                {
                    con.Open();
                    string query = "SELECT m.id_medico, u.nombre, u.apellido, m.especialidad " +
                                   "FROM usuarios u " +
                                   "JOIN medicos m USING (id_usuario);";
                    using (var cmd = new Npgsql.NpgsqlCommand(query, con))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var medico = new Medico
                                {
                                    IdMedico = dr.GetInt32(dr.GetOrdinal("id_medico")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    Especialidad = dr.GetString(dr.GetOrdinal("especialidad"))
                                };
                                lista.Add(medico);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores (puede ser logging, rethrow, etc.)
                        throw new Exception("Error al listar médicos: " + ex.Message);
            }
            return lista;
        }
    }
}
