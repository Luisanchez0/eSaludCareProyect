using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_ConsultasCitas
    {
        private readonly CD_ConsultasCitas datos;

        public CN_ConsultasCitas(string cadenaConexion)
        {
            datos = new CD_ConsultasCitas(cadenaConexion);
        }

        // Obtener todas las citas agendadas
        public List<CitaAgendadaDTO> ObtenerCitasAgendadas()
        {
            return datos.ListarCitas();
        }

        // Obtener una cita por ID
        public CitaAgendadaDTO ObtenerCitaPorId(int id)
        {
            return datos.BuscarCitaPorId(id);
        }

        // Crear una nueva cita
        public bool CrearCitaAgendada(CitaAgendadaDTO nuevaCita)
        {
            return datos.InsertarCita(nuevaCita);
        }

        // Actualizar una cita existente
        public bool ActualizarCitaAgendada(int id, CitaAgendadaDTO citaActualizada)
        {
            return datos.ActualizarCita(id, citaActualizada);
        }

        // Eliminar una cita por ID
        public bool EliminarCitaAgendada(int id)
        {
            return datos.EliminarCita(id);
        }
    }
}