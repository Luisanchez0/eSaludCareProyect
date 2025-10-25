using CapaEntidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Citas
    {
        private ConectionBD conexion = new ConectionBD();


        public bool RegistrarCita(CitaMedica cita)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();

                //verificar eistencia de paciente
                using (var cmdVerificarPaciente = new NpgsqlCommand("SELECT COUNT(*) FROM pacientes WHERE id_paciente = @id_paciente", con))
                {
                    cmdVerificarPaciente.Parameters.AddWithValue("id_paciente", cita.IdPaciente);
                    var existePaciente = (long)cmdVerificarPaciente.ExecuteScalar() > 0;


                    if (!existePaciente)
                    {
                        throw new Exception("El paciente no existe.");
                    }

                }

                string query = @"INSERT INTO citas (id_paciente, id_medico, fecha, hora, estado, motivo, fecha_registro)
                 VALUES (@id_paciente, @id_medico, @fecha, @hora, @estado, @motivo, @fecha_registro)";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("hora", cita.Hora);
                    cmd.Parameters.AddWithValue("id_paciente", cita.IdPaciente);
                    cmd.Parameters.AddWithValue("id_medico", cita.IdMedico);
                    cmd.Parameters.AddWithValue("estado", cita.Estado ?? "pendiente");
                    cmd.Parameters.AddWithValue("motivo", cita.Motivo ?? "");
                    cmd.Parameters.AddWithValue("@fecha_registro", DateTime.Now);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        public List<CitaMedica> ListarCitas(int idMedico, DateTime fecha)
        {
            List<CitaMedica> lista = new List<CitaMedica>();

            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = @"
                    SELECT fecha
                    FROM citas
                    WHERE id_medico = @idMedico
                      AND DATE(fecha) = @fecha";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("idMedico", idMedico);
                    cmd.Parameters.AddWithValue("fecha", fecha.Date);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime fechaHora = reader.GetDateTime(0);
                            lista.Add(new CitaMedica
                            {
                                Fecha = fechaHora.Date,
                                Hora = fechaHora.TimeOfDay
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // Obtener citas por paciente
        /*        public List<CitaMedica> ListarPorPaciente(int idPaciente)
                {
                    List<Cita> lista = new List<CitaMedica>();
                    using (SqlConnection cn = new SqlConnection(conexion))
                    {
                        string query = "SELECT * FROM Citas WHERE IdPaciente = @IdPaciente";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);
                        cn.Open();

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lista.Add(new Cita
                            {
                                IdCita = Convert.ToInt32(dr["IdCita"]),
                                IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                IdMedico = Convert.ToInt32(dr["IdMedico"]),
                                FechaCita = Convert.ToDateTime(dr["FechaCita"]),
                                HoraCita = dr["HoraCita"].ToString(),
                                Motivo = dr["Motivo"].ToString(),
                                Estado = dr["Estado"].ToString()
                            });
                        }
                    }
                    return lista;
                }
        */

        public List<CitaMedica> ObtenerCitasPorPaciente(int idPaciente)
        {
            var lista = new List<CitaMedica>();

            using (var con = conexion.Conectar())
            {
                con.Open();
                    string query = @"SELECT c.id_cita, c.fecha, c.hora, c.estado, 
                                    COALESCE(m.nombre || ' ' || m.apellido, '') AS doctor
                                    FROM citas c
                                    LEFT JOIN medicos m ON c.id_medico = m.id_medico
                                    WHERE c.id_paciente = @id_paciente
                                    ORDER BY c.fecha DESC";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("id_paciente", idPaciente);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CitaMedica
                            {
                                IdCita = Convert.ToInt32(dr["id_cita"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Hora = TimeSpan.Parse(dr["hora"].ToString()),
                                Estado = dr["estado"].ToString(),
                               // Doctor = dr["doctor"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool ActualizarEstadoCita(int idCita, string nuevoEstado)
        {
            using (var con = conexion.Conectar())
            {
                con.Open();
                string query = "UPDATE citas SET estado = @estado WHERE id_cita = @id_cita";

                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("id_cita", idCita);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }







    }
}
