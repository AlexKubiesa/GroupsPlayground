using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class PermutationGroup : Group
    {
        public PermutationGroup(Guid id, string name, int? size) : base(id, name)
        {
            Size = size;
        }

        public override int? Size { get; }
    }
}