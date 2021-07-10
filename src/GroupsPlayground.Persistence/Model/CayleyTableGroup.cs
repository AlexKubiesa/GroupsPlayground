using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Persistence.Model
{
    public class CayleyTableGroup : Group
    {
        public CayleyTableGroup(Guid id, string name) : base(id, name)
        {
        }

        public IReadOnlyList<GroupElement> Elements { get; set; }

        public IReadOnlyCollection<GroupElementProduct> Products { get; set; }
    }
}