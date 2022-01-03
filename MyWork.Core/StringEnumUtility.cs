using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Core
{
    public class StringEnumUtility
    {
        private StringEnumUtility() { }

        public static string GetStringValue(Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringEnumAttribute[] attrs = fieldInfo.GetCustomAttributes(typeof(StringEnumAttribute), false) as StringEnumAttribute[];
            return (attrs.Length > 0 ? attrs[0].StringValue : "");
        }

        public static object GetEnumValue(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (var name in names)
            {
                if (GetStringValue((Enum)Enum.Parse(enumType, name)).Equals(value) )
                {
                    return Enum.Parse(enumType, name);
                }
            }
            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }
    }
}
