using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Model
{
    public sealed class Group : AggregateRoot
    {
        public Group(Guid id) : base(id)
        {
        }

        public string Name { get; set; }

        public IReadOnlyList<GroupElement> Elements { get; set; }

        public IReadOnlyCollection<GroupElementProduct> Products { get; set; }
    }
}