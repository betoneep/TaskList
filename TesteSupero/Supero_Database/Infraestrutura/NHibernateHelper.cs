using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Supero_Database.Globais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Database.Infraestrutura
{

    public class NHibernateHelper
    {
        public static bool VisualizarScript = false;
        public static bool RestaurarBase = false;

        private static ITransaction _transacao;

        private static ITransaction Transacao
        {
            get
            {
                if (!TransacaoAtiva())
                {
                    _transacao = CreateTransacao();
                }
                return _transacao;
            }
        }

        private static bool TransacaoAtiva()
        {
            try
            {
                if (_transacao.IsActive)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        private static ITransaction CreateTransacao()
        {
            return GetSession().OpenSession().BeginTransaction();
        }

        public static ITransaction GetTransacao()
        {
            return Transacao;
        }

        private static ISessionFactory _session;
        private static ISessionFactory sessionFactory
        {
            get
            {
                if (_session == null)
                {
                    _session = CreateSessionFactory();
                }
                return _session;
            }
        }
        private static ISessionFactory CreateSessionFactory()
        {
            try
            {

                return Fluently.Configure()
                          .Database(MsSqlConfiguration.MsSql2012.ConnectionString(x => x
                              .Server(Parametros.HostServidor)
                              .Username(Parametros.Usuario)
                              .Password(Parametros.Senha)
                              .Database(Parametros.Banco)))
                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Mapeamento.TarefaMap>())
                       .ExposeConfiguration(BuildSchema)
                       .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar ao banco farmafacil -> " + ex.Message);
            }
        }

        private static void BuildSchema(Configuration config)
        {
            try
            {
                new SchemaExport(config)
                               .Create(false, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static ISessionFactory GetSession()
        {
            return sessionFactory;
        }
    }
}
