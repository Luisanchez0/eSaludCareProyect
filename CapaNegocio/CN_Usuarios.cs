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

        public List<UsuarioEntidad> ListarUsuarios()
        {
            var listaUsuarios = objCapaDato.ObtenerUsuarios(); // List<Usuarios>
            return listaUsuarios.Select(u => UsuarioMapper.AEntidad(u)).ToList(); // List<UsuarioEntidad>
        }

        public Usuarios Login(string correo, string contrasena)
        {
            return objCapaDato.ValidarUser(correo, contrasena);
        }

        public void GuardarToken(int id_usuario, string token)
        {
            objCapaDato.GuardarToken(id_usuario, token);
        }

        public bool RegistrarUsuario(UsuarioEntidad usuario)
        {
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            return objCapaDato.InsertarUsuario(usuario);
        }

        public bool ActualizarUsuario(UsuarioEntidad usuario)
        {
            var usuarioConvertido = UsuarioMapper.AModelo(usuario);
            return objCapaDato.ActualizarUsuario(usuarioConvertido);
        }

        public bool EliminarUsuario(int id)
        {
            return objCapaDato.EliminarUsuario(id);
        }


    }


}
