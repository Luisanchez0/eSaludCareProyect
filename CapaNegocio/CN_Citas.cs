using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
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
