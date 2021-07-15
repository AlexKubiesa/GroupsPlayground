using System;
using System.Collections.Generic;

namespace GroupsPlayground.Persistence.Groups.Model
{
    public sealed class PermutationGroup : Group
    {
        public PermutationGroup(Guid id, string name) : base(id, name)
        {
        }

        public IReadOnlyList<Permutation> Generators { get; set; }

        public int? Size { get; set; }
    }
}