using NHibernate;
using Supero_Database.Infraestrutura;
using Supero_Database.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Database.Abstract
{
    public class AbstractRepositorio<T> : IDao<T> where T : class
    {
        public virtual void Save(T entidade)
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(entidade);
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao inserir entidade: " + ex.Message);
                    }
                }
            }
        }

        public virtual void Update(T entidade)
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
                        throw new Exception("Erro ao atualizar entidade: " + ex.Message);
                    }
                }
            }
        }

        public virtual void Delete(T entidade)
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
                        throw new Exception("Erro ao excluir entidade: " + ex.Message);
                    }
                }
            }
        }

        public virtual T GetById(int id)
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    return session.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual IList<T> Get()
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    return (from e in session.Query<T>() select e).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual T FirstOrDefault()
        {
            using (ISession session = NHibernateHelper.GetSession().OpenSession())
            {
                return (from e in session.Query<T>() select e).FirstOrDefault();
            }
        }

        public virtual T LastOrDefault()
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    return (from e in session.Query<T>() select e).ToList().Last();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual bool DeleteByWhere(Expression<Func<T, bool>> where)
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    try
                    {
                        Delete(session.Query<T>().Where<T>(where).FirstOrDefault());
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual IQueryable<T> GetAll()
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    return (from e in session.Query<T>() select e).AsQueryable();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T GetById(long id)
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    return session.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual int GetValueSequence(string parSequence)
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    var dialect = session.GetSessionImplementation().Factory.Dialect;
                    var sqlQuery = dialect.GetSequenceNextValString(parSequence);
                    return int.Parse(session.CreateSQLQuery(sqlQuery).UniqueResult().ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public virtual IList<T> GetByWhere(Expression<Func<T, bool>> where)
        {
            try
            {
                using (ISession session = NHibernateHelper.GetSession().OpenSession())
                {
                    return session.Query<T>().Where<T>(where).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

