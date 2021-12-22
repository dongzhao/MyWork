using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Core
{
    [AttributeUsage(AttributeTargets.All)]
    public class ModelAttribute : Attribute
    {
        public Boolean Searchable { get; set; }
        public Boolean EagerLoading { get; set; }
    }
}
