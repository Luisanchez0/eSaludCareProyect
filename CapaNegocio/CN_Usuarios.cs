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

using BCrypt.Net;


namespace CapaNegocio
{
    public class CN_Usuarios
    {

        private CD_Usuarios objCapaDato = new CD_Usuarios();

        //Login
        public Usuarios Login(string correo, string contrasena)
        {
            return objCapaDato.ValidarUser(correo, contrasena);
        }
        //guardar token
        public void GuardarToken(int id_usuario, string token)
        {
            objCapaDato.GuardarToken(id_usuario, token);
        }

        //registro de usuarios
        public bool RegistrarUsuario(UsuarioEntidad usuario)
        {
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            return objCapaDato.InsertarUsuario(usuario);
        }



    }


}
