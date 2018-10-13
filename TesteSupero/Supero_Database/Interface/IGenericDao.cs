using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Database.Interface
{
    public interface IGenericDao
    {
        void Save(object objeto);
        void Save<T>(T entidade) where T : class;
        void Update<T>(T entidade) where T : class;
        void Delete<T>(T entidade);
        T GetById<T>(int id) where T : class;
        IList<T> Get<T>() where T : class;
    }
}
