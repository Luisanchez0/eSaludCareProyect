using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models.Helpers
{
    public class UsuarioAdapter
    {
        public static UsuarioEntidad Convertir(Usuarios u)
        {
            return new UsuarioEntidad
            {
                id_usuario = u.id_usuario,
                nombre = u.nombre,
                apellido = u.apellido,
                correo = u.correo,
                contrasena = u.contrasena,
                telefono = u.telefono,
                rol = u.rol,
                fecha_registro = u.fecha_registro
            };
        }

    }
}