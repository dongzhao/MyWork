using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Core
{
    [AttributeUsage(AttributeTargets.All)]
    public class StringEnumAttribute : Attribute
    {
        public string StringValue { get; protected set; }

        public StringEnumAttribute(string value)
        {
            this.StringValue = value;
        }
    }
}
