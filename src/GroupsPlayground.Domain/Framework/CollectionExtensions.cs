using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Domain.Framework
{
    public static class CollectionExtensions
    {
        public static bool AreDistinct<T>(this IEnumerable<T> collection)
        {
            var set = new HashSet<T>();
            return collection.All(item => set.Add(item));
        }
    }
}