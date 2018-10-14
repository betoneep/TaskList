using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Database.Globais
{
    public static class Parametros
    {

        public static string HostServidor
        {
            get { return new AppSettingsReader().GetValue("HostDb", typeof(System.String)).ToString(); }
        }

        public static string Usuario
        {
            get { return new AppSettingsReader().GetValue("UserDb", typeof(System.String)).ToString(); }
        }

        public static string Senha
        {
            get { return new AppSettingsReader().GetValue("PassDb", typeof(System.String)).ToString(); }
        }

        public static string Banco
        {
            get { return new AppSettingsReader().GetValue("NameDb", typeof(System.String)).ToString(); }
        }

        public static string Schema
        {
            get { return new AppSettingsReader().GetValue("SchemaDb", typeof(System.String)).ToString(); }
        }
    }
}
