using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public interface IRepository<T, ID> where T : class
    {
        T GetById(ID id);
        IEnumerable<T> GetAll();
        ID Create(T obj);
        void Update(T obj);
        void Delete(ID id);
        //IEnumerable<T> Search(params Expression<Func<T, object>>[] peoperties);
    }
}
