using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class PartialBinaryOperation : ValueObject<PartialBinaryOperation>
    {
        private readonly ValueList<Symbol> symbols;
        private readonly ValueList<ValueList<Symbol>> products;

        public PartialBinaryOperation(ValueList<Symbol> symbols, ValueList<ValueList<Symbol>> products)
        {
            this.symbols = symbols ?? throw new ArgumentNullException(nameof(symbols));
            this.products = products ?? throw new ArgumentNullException(nameof(products));
        }

        protected override bool EqualsInternal(PartialBinaryOperation other) =>
            symbols.Equals(other.symbols) && products.Equals(other.products);

        protected override int GetHashCodeInternal() =>
            HashCode.Combine(symbols.GetHashCode(), products.GetHashCode());

        public Symbol Combine(Symbol first, Symbol second)
        {
            if (first == null || second == null)
                return null;

            int firstIndex = symbols.IndexOf(first);
            if (firstIndex < 0)
                return null;

            int secondIndex = symbols.IndexOf(second);
            if (secondIndex < 0)
                return null;

            return products[firstIndex][secondIndex];
        }

        public bool IsTotal() => products.SelectMany(x => x).All(x => x != null);

        private void EnsureTotal()
        {
            if (!IsTotal())
                throw new InvalidOperationException("Binary operation is not total.");
        }

        public bool IsClosed()
        {
            EnsureTotal();   
            return products.SelectMany(x => x).All(symbols.Contains);
        }

        private void EnsureClosed()
        {
            if (!IsClosed())
                throw new InvalidOperationException("Binary operation is not closed.");
        }

        public bool IsAssociative()
        {
            // No need to call EnsureTotal, since that's called by EnsureClosed.
            EnsureClosed();
            return symbols
                .SelectMany(first => symbols.SelectMany(second => symbols.Select(third => (first, second, third))))
                .All(x => Combine(Combine(x.first, x.second), x.third) == Combine(x.first, Combine(x.second, x.third)));
        }

        public Symbol IdentityElement()
        {
            // No need to call EnsureTotal, since that's called by EnsureClosed.
            EnsureClosed();
            return symbols.FirstOrDefault(candidate =>
                symbols.All(other =>
                    (Combine(candidate, other) == other) && (Combine(other, candidate) == other)));
        }

        public bool HasIdentityElement() => IdentityElement() != null;

        private void EnsureIdentityElement()
        {
            if (!HasIdentityElement())
                throw new InvalidOperationException("Binary operation has no identity element.");
        }

        public bool HasInverses()
        {
            // No need to call EnsureTotal or EnsureClosed, since we call EnsureIdentityElement.
            EnsureIdentityElement();
            var identityElement = IdentityElement();
            return symbols.All(element =>
                symbols.Any(candidate =>
                    (Combine(element, candidate) == identityElement) && (Combine(candidate, element) == identityElement)));
        }
    }
}