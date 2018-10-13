using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Database.Interface
{
    public interface IDao<T>
    {
        void Save(T entidade);
        void Update(T entidade);
        void Delete(T entidade);
        T GetById(int id);
        T GetById(long id);
        IList<T> Get();
        int GetValueSequence(string parSequence);
        IList<T> GetByWhere(Expression<Func<T, bool>> where);
    }
}
