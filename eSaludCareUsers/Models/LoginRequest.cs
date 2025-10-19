using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; }

    }
}