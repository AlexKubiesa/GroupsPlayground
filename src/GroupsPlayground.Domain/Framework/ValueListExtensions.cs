using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Domain.Framework
{
    public static class ValueListExtensions
    {
        public static ValueList<T> ToValueList<T>(this IEnumerable<T> source) => new ValueList<T>(source.ToArray());
    }
}