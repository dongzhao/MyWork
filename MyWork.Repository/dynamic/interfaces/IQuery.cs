using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public interface IQuery<T> where T : class
    {
        //IEnumerable<T> Execute(Func<T, bool> func);
        Func<T, bool> Filter();
        IEnumerable<T> Execute(List<QueryItem> items);
    }
}
