using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Domain.Framework
{
    public static class ValueListExtensions
    {
        public static ValueList<T> ToValueList<T>(this IEnumerable<T> source)
        {
            if (source is ValueList<T> valueList)
                return valueList;

            return new ValueList<T>(source.ToArray());
        }
    }
}