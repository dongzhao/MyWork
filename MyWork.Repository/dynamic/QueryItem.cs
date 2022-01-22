using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public class QueryItem
    {
        public string ItemName { get; set; }
        public string ItemValue { get; set; }
        public string Operator { get; set; }
    }

    public enum QueryOperatorEnum
    {
        AND,
        OR,
    }
}
