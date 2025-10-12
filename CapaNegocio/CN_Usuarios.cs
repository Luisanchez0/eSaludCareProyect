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
    
        public Usuarios Login(string correo, string contraseña)
        {
            return objUsuarios.Login(correo, contraseña);
        }
    }
}
