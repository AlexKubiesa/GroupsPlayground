using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class BinaryOperation : ValueObject<BinaryOperation>
    {
        private readonly ValueList<Symbol> domain;
        private readonly ValueList<ValueList<Symbol>> products;
        private readonly HashSet<Symbol> range;

        public BinaryOperation(ValueList<Symbol> domain, ValueList<ValueList<Symbol>> products)
        {
            this.domain = domain ?? throw new ArgumentNullException(nameof(domain));
            this.products = products ?? throw new ArgumentNullException(nameof(products));

            range = products
                .SelectMany(x => x)
                .Where(x => x != null)
                .ToHashSet();
        }

        public IReadOnlyCollection<Symbol> Domain => domain;
        public IReadOnlyCollection<Symbol> Range => range;

        protected override bool EqualsInternal(BinaryOperation other) =>
            domain.Equals(other.domain) && products.Equals(other.products);

        protected override int GetHashCodeInternal() =>
            HashCode.Combine(domain, products);

        public Symbol Combine(Symbol first, Symbol second)
        {
            if (first == null || second == null)
                return null;

            int firstIndex = domain.IndexOf(first);
            if (firstIndex < 0)
                return null;

            int secondIndex = domain.IndexOf(second);
            if (secondIndex < 0)
                return null;

            return products[firstIndex][secondIndex];
        }

        public bool IsFullyDefined() => products.SelectMany(x => x).All(x => x != null);
    }
}