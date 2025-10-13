using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

using CapaDatos;
using Npgsql;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CapaNegocio
{
    public class CN_Usuarios
    {

        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public Usuarios Login(string correo, string contrasena)
        {
            return objCapaDato.ValidarUser(correo, contrasena);
        }

        public void GuardarToken(int id_usuario, string token)
        {
            objCapaDato.GuardarToken(id_usuario, token);
        }
    }
}
