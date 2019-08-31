using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.Core.Extensions
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

        public static IEnumerable<IEnumerable<T>> Split<T>(this ICollection<T> self, int chunkSize)
        {
            var splitList = new List<List<T>>();

            var chunkCount = (int)Math.Ceiling((double)self.Count / (double)chunkSize);

            for (int c = 0; c < chunkCount; c++)
            {
                var skip = c * chunkSize;
                var take = skip + chunkSize;
                var chunk = new List<T>(chunkSize);

                for (int e = skip; e < take && e < self.Count; e++)
                {
                    chunk.Add(self.ElementAt(e));
                }

                splitList.Add(chunk);
            }

            return splitList;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable,Action<T> action)
        {
            foreach (T item in enumerable) { action(item); }
        }
    }
}


