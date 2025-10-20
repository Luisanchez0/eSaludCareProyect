using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaEntidad
{
    public static class UsuarioMapper
    {
        public static UsuarioEntidad AEntidad(Usuarios u)
        {
            return new UsuarioEntidad
            {
                IdUsuario = u.id_usuario,
                Nombre = u.nombre,
                Apellido = u.apellido,
                Correo = u.correo,
                Contrasena = u.contrasena,
                Telefono = u.telefono,
                Rol = u.rol,
                FechaRegistro = u.fecha_registro
            };
        }

        public static Usuarios AModelo(UsuarioEntidad u)
        {
            return new Usuarios
            {
                id_usuario = u.IdUsuario,
                nombre = u.Nombre,
                apellido = u.Apellido,
                correo = u.Correo,
                contrasena = u.Contrasena,
                telefono = u.Telefono,
                rol = u.Rol,
                fecha_registro = u.FechaRegistro,
                activo = true,
                fecha_actualizacion = DateTime.Now,
                token = ""
            };
        }

        public static PerfilUsuarioDTO APerfilDTO(Usuarios u)
        {
            if (u == null) return null;

            return new PerfilUsuarioDTO
            {
                Id = u.id_usuario,
                Nombre = u.nombre + " " + u.apellido,
                Correo = u.correo,
                Telefono = u.telefono,
                Rol = u.rol,
                
            };
        }

       



    }
}
