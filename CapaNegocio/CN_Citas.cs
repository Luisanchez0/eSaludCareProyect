using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace CapaNegocio
{
    public class CN_Citas
    {
        private CD_Citas _citaDatos = new CD_Citas();
        public bool RegistrarCita(CapaEntidad.CitaMedica cita)
        {
            if (cita.IdPaciente <= 0 || cita.IdMedico <= 0)
                throw new ArgumentException("El paciente y el médico son obligatorios.");
            if (cita.Fecha == default(DateTime))
                throw new ArgumentException("La fecha de la cita es obligatoria.");

            cita.Estado = cita.Estado ?? "pendiente";
            //cita.FechaRegistro = DateTime.Now;
            return _citaDatos.RegistrarCita(cita);
        }

        public List<CitaMedica> ListarCitas(int idMedico, DateTime fecha)
        {

            return _citaDatos.ListarCitas(idMedico, fecha);
        }


        public List<CitaMedica> ObtenerCitasPorPaciente(int idPaciente)
        {
            return _citaDatos.ObtenerCitasPorPaciente(idPaciente);
        }


        public List<HorarioApi> ObtenerHorariosDisponibles(int idMedico, DateTime fecha)
        {
            var diaSemana = (int)fecha.DayOfWeek;
            if (diaSemana == 0) diaSemana = 7; // Domingo = 7

            var turnos = _citaDatos.ObtenerTurnosMedico(idMedico, diaSemana);
            var citas = _citaDatos.ObtenerCitasPorFecha(idMedico, fecha);

            var horarios = new List<HorarioApi>();

            foreach (var turno in turnos)
            {
                var hora = turno.inicio;
                while (hora < turno.fin)
                {
                    horarios.Add(new HorarioApi
                    {
                        Hora = hora.ToString(@"hh\:mm"),
                        Ocupado = citas.Contains(hora)
                    });
                    hora = hora.Add(TimeSpan.FromMinutes(30));
                }
            }

            return horarios.OrderBy(h => h.Hora).ToList();
        }
        /*
        public bool actualizarEstadoCita(int idCita, string nuevoEstado)
        {
            if (idCita <= 0)
                throw new ArgumentException("El ID de la cita es obligatorio.");
            if (string.IsNullOrEmpty(nuevoEstado))
                throw new ArgumentException("El nuevo estado es obligatorio.");
            return _citaDatos.actualizarEstadoCita(idCita, nuevoEstado);
        }
        */

    }
}
*/
