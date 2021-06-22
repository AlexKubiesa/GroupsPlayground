using System.Collections.Generic;

namespace GroupsPlayground.Domain.Framework
{
    public static class IndexExtensions
    {
        private const int NotFound = -1;

        public static int IndexOf<T>(this IReadOnlyList<T> list, T item, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;

            for (int i = 0; i < list.Count; ++i)
            {
                if (comparer.Equals(list[i], item))
                    return i;
            }

            return NotFound;
        }
    }
}