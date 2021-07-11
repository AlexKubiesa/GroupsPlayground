using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Cycle : ValueObject<Cycle>
    {
        private readonly ValueList<int> elements;

        public Cycle(ValueList<int> elements)
        {
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        protected override bool EqualsInternal(Cycle other) => elements.Equals(other.elements);

        protected override int GetHashCodeInternal() => elements.GetHashCode();

        public override string ToString() => string.Join(',', elements.Select(x => x.ToString()));
    }
}