using MyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public class GenericRepository<T, ID> : AbstractRepository<T, ID>, IRepository<T, ID> where T : class
    {

        public GenericRepository(MyWorkDbContext context) 
                : base(context)
        {
        }


        public IEnumerable<T> SearchWith(params Expression<Func<T, object>>[] properties)
        {
            IQueryable<T> query = dbSet;
            if (properties != null && properties.Length > 0)
            {
                query = properties.Aggregate(query, (current, property) => current.Include(property));
            }
            return query.ToList<T>();
        }
    }
}
