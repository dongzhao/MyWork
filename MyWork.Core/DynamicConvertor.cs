using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Core
{
    public class DynamicConvertor
    {
        private DynamicConvertor() { }

        private static readonly Dictionary<string, Func<string, object>> list =
            new Dictionary<string, Func<string, object>>
        {
            { typeof (int).FullName, v => Convert.ToInt32(v) },
            { typeof (Decimal).FullName, v => Convert.ToDecimal(v) },
            { typeof (double).FullName, v => Convert.ToDouble( v) },
            { typeof (string).FullName, v => Convert.ToString(v) },
            { typeof (DateTime).FullName, v => Convert.ToDateTime(v) },
            { typeof (bool).FullName, v => Convert.ToBoolean(v) },
        };

        public static T To<T>(string value)
        {
            var target = typeof(T).FullName;
            if (list.ContainsKey(target))
                return (T)list[target](value);

            throw new NotSupportedException("Unsupported type");
        }


    }
}
