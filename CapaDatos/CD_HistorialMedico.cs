using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using Npgsql;

namespace CapaDatos
{
    public class CD_HistorialMedico
    {
        private ConectionBD conexion = new ConectionBD();

        public List<HistorialMedico> ListarPorPaciente(int id_paciente)
        {
            var lista = new List<HistorialMedico>();
            using (var con = conexion.Conectar())
            {
                con.Open();

                string query = "SELECT * FROM historial_medico WHERE id_paciente = @id_paciente";
                using (var cmd = new NpgsqlCommand(query, con)) 
                { 
                    cmd.Parameters.AddWithValue("@id_paciente", id_paciente);
                    using (var dr = cmd.ExecuteReader()) 
                    {
                        while (dr.Read()) 
                        {
                            lista.Add(new HistorialMedico
                            {
                                id_historial_medico = Convert.ToInt32(dr["id_historial_medico"]),
                                id_paciente = Convert.ToInt32(dr["id_paciente"]),
                                enfermedades_previas = dr["enfermedades_previas"].ToString(),
                                alergias = dr["alergias"].ToString(),
                                cirugias = dr["cirugias"].ToString(),
                                medicamentos_actuales = dr["medicamentos_actuales"].ToString(),
                                antecedentes_familiares = dr["antecedentes_familiares"].ToString(),
                                habitos = dr["habitos"].ToString(),
                                observaciones_medico = dr["observaciones_medico"].ToString(),
                                fecha_actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"])
                            });
                        }
                    
                    }
                }

            }
            return lista;

        }

        public bool Guardar(HistorialMedico h)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"INSERT INTO historial_medico"
                                + "(id_paciente, enfermedades_previas, alergias, cirugias, medicamentos_actuales, antecedentes_familiares, habitos) "
                                + "VALUES (@id_paciente, @enfermedades_previas, @alergias, @cirugias, @medicamentos_actuales, @antecedentes_familiares, @habitos)";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_paciente", h.id_paciente);
                    cmd.Parameters.AddWithValue("@enfermedades_previas", h.enfermedades_previas);
                    cmd.Parameters.AddWithValue("@alergias", h.alergias);
                    cmd.Parameters.AddWithValue("@cirugias", h.cirugias);
                    cmd.Parameters.AddWithValue("@medicamentos_actuales", h.medicamentos_actuales);
                    cmd.Parameters.AddWithValue("@antecedentes_familiares", h.antecedentes_familiares);
                    cmd.Parameters.AddWithValue("@habitos", h.habitos);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }

        }

        public bool Actualizar(HistorialMedico h)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"UPDATE historial_medico SET "
                                + "enfermedades_previas = @enfermedades_previas, "
                                + "alergias = @alergias, "
                                + "cirugias = @cirugias, "
                                + "medicamentos_actuales = @medicamentos_actuales, "
                                + "antecedentes_familiares = @antecedentes_familiares, "
                                + "habitos = @habitos, "
                                + "observaciones_medico = @observaciones_medico, "
                                + "fecha_actualizacion = NOW() "
                                + "WHERE id_historial_medico = @id_historial_medico";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_paciente", h.id_paciente);
                    cmd.Parameters.AddWithValue("@enfermedades_previas", h.enfermedades_previas);
                    cmd.Parameters.AddWithValue("@alergias", h.alergias);
                    cmd.Parameters.AddWithValue("@cirugias", h.cirugias);
                    cmd.Parameters.AddWithValue("@medicamentos_actuales", h.medicamentos_actuales);
                    cmd.Parameters.AddWithValue("@antecedentes_familiares", h.antecedentes_familiares);
                    cmd.Parameters.AddWithValue("@habitos", h.habitos);
                    cmd.Parameters.AddWithValue("@observaciones_medico",(object)h.observaciones_medico ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_historial_medico", h.id_historial_medico);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


    }
}
