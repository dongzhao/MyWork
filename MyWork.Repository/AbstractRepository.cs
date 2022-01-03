using MyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public abstract class AbstractRepository<T, ID> : IRepository<T, ID> where T : class
    {
        protected readonly MyWorkDbContext ctx;
        protected readonly DbSet<T> dbSet;
        public AbstractRepository(MyWorkDbContext context)
        {
            this.ctx = context;
            this.dbSet = context.Set<T>();
        }

        public virtual ID Create(T obj)
        {
            dbSet.Add(obj);
            ctx.SaveChanges();
            return (obj as IEntity<ID>).Id;
        }

        public virtual void Delete(ID id)
        {
            T obj = dbSet.Find(id);
            if (ctx.Entry(obj).State == EntityState.Detached)
            {
                dbSet.Attach(obj);
            }
            dbSet.Remove(obj);
            ctx.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList<T>();
        }

        public virtual T GetById(ID id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(T obj)
        {
            dbSet.Attach(obj);
            ctx.Entry(obj).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
