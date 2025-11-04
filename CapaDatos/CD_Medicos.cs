using CapaEntidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Medicos
    {

        private ConectionBD conexion = new ConectionBD();

        public List<MedicoAsignado> ListarMedicos()
        {
            List<MedicoAsignado> lista = new List<MedicoAsignado>();
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
                                var medico = new MedicoAsignado
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


        public Medico ObtenerPorUsuario(int idUsuario)
        {
            using (var conn = conexion.Conectar())
            {
                conn.Open();
                string query = "SELECT id_medico, id_usuario, especialidad FROM medicos WHERE id_usuario = @id_usuario LIMIT 1;";

//                string query = "SELECT id_medico, id_usuario, nombre, especialidad FROM medico WHERE id_usuario = @id_usuario LIMIT 1;";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Medico
                            {
                                IdMedico = reader.GetInt32(0),
                                IdUsuario = reader.GetInt32(1),
                                //nombre = reader.GetString(2),
                                Especialidad = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return null;
        }


    }
}
