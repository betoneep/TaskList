using NHibernate;
using Supero_Database.Infraestrutura;
using Supero_Database.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Database.Abstract
{
    public abstract class AbstractDao : IGenericDao
    {
        public void Save(object objeto)
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                session.SaveOrUpdate(objeto);
            }
        }

        public void Save<T>(T entidade) where T : class
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }

                        throw new Exception("Erro ao inserir: " + ex.Message);
                    }
                }
            }
        }

        public void Update<T>(T entidade) where T : class
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao atualizar: " + ex.Message);
                    }
                }
            }
        }

        public void Delete<T>(T entidade)
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao excluir: " + ex.Message);
                    }
                }
            }
        }

        public T GetById<T>(int id) where T : class
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public IList<T> Get<T>() where T : class
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                return (from e in session.Query<T>() select e).ToList();
            }
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                return (from e in session.Query<T>() select e).AsQueryable();
            }
        }
    }
}
