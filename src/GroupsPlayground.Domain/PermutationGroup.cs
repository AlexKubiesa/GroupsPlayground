using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class PermutationGroup : Group
    {
        public PermutationGroup(Guid id, string name, ValueList<Permutation> generators, int? size) : base(id, name)
        {
            Generators = generators ?? throw new ArgumentNullException(nameof(generators));
            Size = size;
        }

        public ValueList<Permutation> Generators { get; }
        public override int? Size { get; }

        public override void ComputeSize()
        {
            base.ComputeSize();
        }
    }
}