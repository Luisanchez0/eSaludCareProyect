using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    public class Medico
    {
        public int id_medico { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; } 


    }
}