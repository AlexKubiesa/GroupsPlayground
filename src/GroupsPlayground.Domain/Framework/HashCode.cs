using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Domain.Framework
{
    public static class HashCode
    {
        public static int Combine(int first, int second) => first ^ (second * 397);

        public static int Combine(IEnumerable<int> hashCodes) => hashCodes.Aggregate(0, Combine);
    }
}