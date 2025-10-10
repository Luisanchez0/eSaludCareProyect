using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSaludCareUsers.Models
{
    public class Paciente
    {
        public int id_paciente { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }

    }
}