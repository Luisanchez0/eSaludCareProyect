using System;

namespace CapaEntidad
{
    public class CitaAgendadaDTO
    {
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public int IdMedico { get; set; }
        public string NombreMedico { get; set; }
        public DateTime? Fecha { get; set; }       // Nullable por si hay registros incompletos
        public TimeSpan? Hora { get; set; }        // Nullable por si hay registros incompletos
        public string Estado { get; set; }
    }
}