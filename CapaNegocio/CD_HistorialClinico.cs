using CapaDatos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaNegocio
{
    public class CD_HistorialClinico
    {
        private ConectionBD conexion = new ConectionBD();
        public bool Guardar(HistorialClinico h)
        {
            using (var conn = conexion.Conectar())
            {
                conn.Open();
                string query = @"
                    INSERT INTO historial_clinico (
                        id_paciente, id_medico, id_cita, 
                        motivo_consulta, padecimiento_actual,
                        antecedentes_personales, antecedentes_familiares,
                        peso, talla, presion_arterial, frecuencia_cardiaca, frecuencia_respiratoria, temperatura, exploracion_general,
                        diagnostico, pronostico, tratamiento
                    )
                    VALUES (
                        @id_paciente, @id_medico, @id_cita,
                        @motivo_consulta, @padecimiento_actual,
                        @antecedentes_personales, @antecedentes_familiares,
                        @peso, @talla, @presion_arterial, @frecuencia_cardiaca, @frecuencia_respiratoria, @temperatura, @exploracion_general,
                        @diagnostico, @pronostico, @tratamiento
                    );";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_paciente", h.id_paciente);
                    cmd.Parameters.AddWithValue("@id_medico", h.id_medico);
                    cmd.Parameters.AddWithValue("@id_cita", (object)h.id_cita ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@motivo_consulta", h.motivo_consulta);
                    cmd.Parameters.AddWithValue("@padecimiento_actual", (object)h.padecimiento_actual ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@antecedentes_personales", (object)h.antecedentes_personales ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@antecedentes_familiares", (object)h.antecedentes_familiares ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@peso", (object)h.peso ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@talla", (object)h.talla ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@presion_arterial", (object)h.presion_arterial ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@frecuencia_cardiaca", (object)h.frecuencia_cardiaca ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@frecuencia_respiratoria", (object)h.frecuencia_respiratoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@temperatura", (object)h.temperatura ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@exploracion_general", (object)h.exploracion_general ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@diagnostico", h.diagnostico);
                    cmd.Parameters.AddWithValue("@pronostico", (object)h.pronostico ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tratamiento", (object)h.tratamiento ?? DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<HistorialClinico> ListarPorPaciente(int id_paciente)
        {
            var lista = new List<HistorialClinico>();
            using (var conn = conexion.Conectar())
            {
                conn.Open();
                string query = "SELECT * FROM historial_clinico WHERE id_paciente = @id_paciente ORDER BY fecha_registro DESC";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_paciente", id_paciente);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new HistorialClinico
                            {
                                id_historial = Convert.ToInt32(dr["id_historial"]),
                                id_paciente = Convert.ToInt32(dr["id_paciente"]),
                                id_medico = Convert.ToInt32(dr["id_medico"]),
                                id_cita = dr["id_cita"] == DBNull.Value ? (int?)null : (int?)Convert.ToInt32(dr["id_cita"]),
                                motivo_consulta = dr["motivo_consulta"].ToString(),
                                padecimiento_actual = dr["padecimiento_actual"].ToString(),
                                antecedentes_personales = dr["antecedentes_personales"].ToString(),
                                antecedentes_familiares = dr["antecedentes_familiares"].ToString(),
                                peso = dr["peso"] == DBNull.Value ? (decimal?)null : (decimal?)Convert.ToDecimal(dr["peso"]),
                                talla = dr["talla"] == DBNull.Value ? (decimal?)null : (decimal?)Convert.ToDecimal(dr["talla"]),
                                presion_arterial = dr["presion_arterial"].ToString(),
                                frecuencia_cardiaca = dr["frecuencia_cardiaca"] == DBNull.Value ? (int?)null : (int?)Convert.ToInt32(dr["frecuencia_cardiaca"]),
                                frecuencia_respiratoria = dr["frecuencia_respiratoria"] == DBNull.Value ? (int?)null : (int?)Convert.ToInt32(dr["frecuencia_respiratoria"]),
                                temperatura = dr["temperatura"] == DBNull.Value ? (decimal?)null : (decimal?)Convert.ToDecimal(dr["temperatura"]),
                                exploracion_general = dr["exploracion_general"].ToString(),
                                diagnostico = dr["diagnostico"].ToString(),
                                pronostico = dr["pronostico"].ToString(),
                                tratamiento = dr["tratamiento"].ToString(),
                                fecha_registro = Convert.ToDateTime(dr["fecha_registro"]),
                                fecha_actualizacion = Convert.ToDateTime(dr["fecha_actualizacion"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
