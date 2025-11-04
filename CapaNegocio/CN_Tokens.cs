using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Tokens
    {
        private readonly CD_Tokens objCapaDato = new CD_Tokens();
        public int ObtenerIdUsuarioPorToken(string token)
        {
            return objCapaDato.ObtenerIdUsuarioPorToken(token);
        }
    }
}
