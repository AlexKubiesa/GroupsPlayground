using System;
using System.Collections.Generic;

namespace GroupsPlayground.Domain.Framework
{
    public static class ListExtensions
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

        public static void RotateLeft<T>(this IList<T> list, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            count %= list.Count;

            if (count == 0)
                return;

            var buffer = new T[count];
            Copy(list, buffer, length: count);
            Copy(list, list, index: count, destinationIndex: 0);
            Copy(buffer, list, index: 0, destinationIndex: list.Count - count);
        }

        private static void Copy<T>(IList<T> list, IList<T> destination) =>
            Copy(list, destination, 0, 0, list.Count);

        private static void Copy<T>(IList<T> list, IList<T> destination, int length) =>
            Copy(list, destination, 0, 0, length);

        private static void Copy<T>(IList<T> list, IList<T> destination, int index, int destinationIndex) =>
            Copy(list, destination, index, destinationIndex, list.Count);

        private static void Copy<T>(IList<T> list, IList<T> destination, int index, int destinationIndex,
            int length)
        {
            for (int offset = 0; offset < length - index; offset++)
                destination[destinationIndex + offset] = list[index + offset];
        }
    }
}