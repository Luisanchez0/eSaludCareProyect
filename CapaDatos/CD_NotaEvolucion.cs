using CapaEntidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_NotaEvolucion
    {
        private ConectionBD conexion = new ConectionBD();
        
        public bool Guardar(NotasEvolucion n)
        {
            using (var conn = conexion.Conectar())
            {
                conn.Open();
                string query = @"
                    INSERT INTO notas_evolucion (
                        id_historial, id_medico, diagnostico, signos_vitales,
                        resultados_estudios, tratamiento_actual, pronostico,
                        observaciones, fecha_nota
                    )
                    VALUES (
                        @id_historial, @id_medico, @diagnostico, @signos_vitales,
                        @resultados_estudios, @tratamiento_actual, @pronostico,
                        @observaciones, @fecha_nota
                    );";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_historial", n.id_historial);
                    cmd.Parameters.AddWithValue("@id_medico", n.id_medico);
                    cmd.Parameters.AddWithValue("@diagnostico", n.diagnostico);
                    cmd.Parameters.AddWithValue("@signos_vitales", (object)n.signos_vitales ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@resultados_estudios", (object)n.resultados_estudios ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tratamiento_actual", (object)n.tratamiento_actual ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@pronostico", (object)n.pronostico ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@observaciones", (object)n.observaciones ?? DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public List<NotasEvolucion> ListarPorHistorial (int id_historial)
        {
            var lista = new List<NotasEvolucion>();
            using (var conn = conexion.Conectar())
            {
                conn.Open();
                string query = "SELECT * FROM notas_evolucion WHERE id_historial = @id_historial ORDER BY fecha_nota DESC";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_historial", id_historial);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new NotasEvolucion
                            {
                                id_nota = Convert.ToInt32(dr["id_nota"]),
                                id_historial = Convert.ToInt32(dr["id_historial"]),
                                id_medico = Convert.ToInt32(dr["id_medico"]),
                                diagnostico = dr["diagnostico"].ToString(),
                                signos_vitales = dr["signos_vitales"].ToString(),
                                resultados_estudios = dr["resultados_estudios"].ToString(),
                                tratamiento_actual = dr["tratamiento_actual"].ToString(),
                                pronostico = dr["pronostico"].ToString(),
                                observaciones = dr["observaciones"].ToString(),
                                fecha_nota = Convert.ToDateTime(dr["fecha_nota"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

    }
}
