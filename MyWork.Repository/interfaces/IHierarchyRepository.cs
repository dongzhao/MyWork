using MyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public interface IHierarchyRepository : IRepository<Hierarchy, int>
    {
        IEnumerable<Hierarchy> GetAllParents();
    }
}
