using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Groups
{
    public sealed class PermutationGroup : Group
    {
        public PermutationGroup(Guid id, string name, ValueList<Permutation> generators, int? size) : base(id, name)
        {
            Generators = generators ?? throw new ArgumentNullException(nameof(generators));
            Size = size;
        }

        public ValueList<Permutation> Generators { get; }

        public override void ComputeSize()
        {
            if (Size.HasValue)
                return;

            var elements = new HashSet<Permutation>();

            foreach (var generator in Generators)
                elements.Add(generator);

            while (true)
            {
                var products =
                    from first in elements
                    from second in elements
                    select first.Multiply(second);
                var newElement = products.FirstOrDefault(x => !elements.Contains(x));

                if (newElement == default)
                    break;

                elements.Add(newElement);
            }

            Size = elements.Count;
        }
    }
}