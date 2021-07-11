using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Cycle : ValueObject<Cycle>
    {
        private readonly ValueList<int> elements;

        public Cycle(IList<int> elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (!elements.AreDistinct())
                throw new ValidationError("Elements in a cycle must be distinct.");

            // Ensure that the smallest element of the cycle is at the start.
            int min = elements.Min();
            int minIndex = elements.IndexOf(min);
            elements.RotateLeft(minIndex);
            this.elements = elements.ToValueList();
        }

        protected override bool EqualsInternal(Cycle other) => elements.Equals(other.elements);

        protected override int GetHashCodeInternal() => elements.GetHashCode();

        public override string ToString() => string.Join(',', elements.Select(x => x.ToString()));
    }
}