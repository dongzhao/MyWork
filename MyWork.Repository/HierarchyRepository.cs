using MyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public class HierarchyRepository : AbstractRepository<Hierarchy, int>, IHierarchyRepository
    {
        public HierarchyRepository(MyWorkDbContext context) : base(context)
        {
        }

        public IEnumerable<Hierarchy> GetAllParents()
        {
            return ctx.HierarchySet.Where(e => e.ParentId == null).ToList();
        }


        public void Delete(int id)
        {
            var obj = GetById(id);
            Stack<Hierarchy> list = new Stack<Hierarchy>();
            list.Push(obj);
            PopulateHierarchyChildren(obj, list);
            foreach (var item in list)
            {
                //ctx.HierarchySet.Remove(item);
                if (ctx.Entry(obj).State == EntityState.Detached)
                {
                    dbSet.Attach(obj);
                }
                dbSet.Remove(obj);
            }
            ctx.SaveChanges();
        }

        private void PopulateHierarchyChildren(Hierarchy obj, Stack<Hierarchy> list)
        {
            foreach (var child in obj.Children)
            {
                list.Push(child);
                PopulateHierarchyChildren(child, list);
            }
        }
    }
}
