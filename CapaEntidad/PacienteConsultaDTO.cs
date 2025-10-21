using System;

namespace CapaEntidad
{
    public class PacienteConsultaDTO
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
    }
}
