using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Negocio.Controle
{
    public static class Controller
    {
        private static ControleTarefa _controleTarefa;

        public static ControleTarefa ControleTarefa()
        {
            if (_controleTarefa == null)
            {
                _controleTarefa = new ControleTarefa();
            }
            return _controleTarefa;
        }
    }
}
