using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Repository
{
    public static class PredicateBuilder
    {
        public static Func<T, bool> True<T>()
        {
            return a => true;
        }

        public static Func<T, bool> False<T>()
        {
            return a => false;
        }

        public static Func<T, bool> Not<T>(this Func<T, bool> predicate)
        {
            return a => !predicate(a);
        }

        public static Func<T, bool> And<T>(this Func<T, bool> left, Func<T, bool> right)
        {
            return a => left(a) && right(a);
        }

        public static Func<T, bool> Or<T>(this Func<T, bool> left, Func<T, bool> right)
        {
            return a => left(a) || right(a);
        }
    }
}
