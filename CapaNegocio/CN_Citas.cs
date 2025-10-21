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
    }
}
