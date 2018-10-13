using Supero_Negocio.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Controller.ControleTarefa().Teste();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
