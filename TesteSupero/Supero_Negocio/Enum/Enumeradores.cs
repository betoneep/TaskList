using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Negocio.Enum
{
    public class Enumeradores
    {
        public enum Status : short
        {
            Ativa = 1,
            Cancelada = 2,
            Nova = 3,
            Excluida = 4
        };
    }
}
