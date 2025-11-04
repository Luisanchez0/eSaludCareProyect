using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Pacientes
    {
        private readonly CD_Pacientes _pacienteDatos = new CD_Pacientes();

        public PerfilPaciente ObtenerPacientePorUsuario(int idUsuario)
        {
            return _pacienteDatos.ObtenerPacientePorUsuario(idUsuario);
        }

        public bool ActualizarPerfilPaciente(PerfilPaciente perfil)
        {
            return _pacienteDatos.ActualizarPerfilPaciente(perfil);
        }
        public List<Paciente> Listar()
        {
            return _pacienteDatos.Listar();
        }


    }
}
