using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Derin.Core.Extensions
{
    public static class Enumerable
    {
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> collection, T source, T replacement)
        {
            IEnumerable<T> collectionWithout = collection;

            if (source != null)
            {
                collectionWithout = collectionWithout.Except(new[] { source });
            }
            return collectionWithout.Union(new[] { replacement });
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable,
               Action<T> action)
        {
            foreach (T item in enumerable) { action(item); }
        }
    }
}


