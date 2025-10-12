using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objUsuarios = new CD_Usuarios();
        public List<Usuarios> Listar()
        {
            return objUsuarios.listar();
        }
    }
}
