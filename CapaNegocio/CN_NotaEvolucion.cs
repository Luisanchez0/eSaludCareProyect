using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_NotaEvolucion
    {
        private readonly CD_NotaEvolucion _datos = new CD_NotaEvolucion();

        public bool Guardar(NotasEvolucion n)
        {
            return _datos.Guardar(n);
        }

        public List<NotasEvolucion> ListarPorHistorial(int id_historial)
        {
            return _datos.ListarPorHistorial(id_historial);
        }
    }
}
