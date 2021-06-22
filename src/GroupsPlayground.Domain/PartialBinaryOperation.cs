using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class PartialBinaryOperation : ValueObject<PartialBinaryOperation>
    {
        private readonly ValueList<Symbol> elements;
        private readonly ValueList<ValueList<Symbol>> products;
        private readonly Lazy<bool> isFullyDefinedLazy;
        private readonly Lazy<bool> isClosedLazy;
        private readonly Lazy<bool> isAssociativeLazy;
        private readonly Lazy<Symbol> identityElementLazy;
        private readonly Lazy<bool> hasInversesLazy;

        public PartialBinaryOperation(ValueList<Symbol> elements, ValueList<ValueList<Symbol>> products)
        {
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
            this.products = products ?? throw new ArgumentNullException(nameof(products));

            isFullyDefinedLazy = new Lazy<bool>(IsFullyDefinedImpl);
            isClosedLazy = new Lazy<bool>(IsClosedImpl);
            isAssociativeLazy = new Lazy<bool>(IsAssociativeImpl);
            identityElementLazy = new Lazy<Symbol>(IdentityElementImpl);
            hasInversesLazy = new Lazy<bool>(HasInversesImpl);
        }

        protected override bool EqualsInternal(PartialBinaryOperation other) =>
            elements.Equals(other.elements) && products.Equals(other.products);

        protected override int GetHashCodeInternal() =>
            HashCode.Combine(elements.GetHashCode(), products.GetHashCode());

        public Symbol Combine(Symbol first, Symbol second)
        {
            if (first == null || second == null)
                return null;

            int firstIndex = elements.IndexOf(first);
            if (firstIndex < 0)
                return null;

            int secondIndex = elements.IndexOf(second);
            if (secondIndex < 0)
                return null;

            return products[firstIndex][secondIndex];
        }

        private bool IsFullyDefinedImpl() => products.SelectMany(x => x).All(x => x != null);

        public bool IsFullyDefined() => isFullyDefinedLazy.Value;

        private void EnsureFullyDefined()
        {
            if (!IsFullyDefined())
                throw new InvalidOperationException("Binary operation is not fully defined!");
        }

        private bool IsClosedImpl()
        {
            EnsureFullyDefined();   
            return products.SelectMany(x => x).All(elements.Contains);
        }

        public bool IsClosed() => isClosedLazy.Value;

        private void EnsureClosed()
        {
            if (!IsClosed())
                throw new InvalidOperationException("Binary operation is not closed.");
        }

        private bool IsAssociativeImpl()
        {
            EnsureFullyDefined();
            EnsureClosed();
            return elements
                .SelectMany(first => elements.SelectMany(second => elements.Select(third => (first, second, third))))
                .All(x => Combine(Combine(x.first, x.second), x.third) == Combine(x.first, Combine(x.second, x.third)));
        }

        public bool IsAssociative() => isAssociativeLazy.Value;

        private Symbol IdentityElementImpl()
        {
            EnsureFullyDefined();
            EnsureClosed();
            return elements.FirstOrDefault(candidate =>
                elements.All(other =>
                    (Combine(candidate, other) == other) && (Combine(other, candidate) == other)));
        }

        public Symbol IdentityElement() => identityElementLazy.Value;

        public bool HasIdentityElement() => IdentityElement() != null;

        private void EnsureIdentityElement()
        {
            if (!HasIdentityElement())
                throw new InvalidOperationException("Binary operation has no identity element.");
        }

        private bool HasInversesImpl()
        {
            EnsureFullyDefined();
            EnsureClosed();
            EnsureIdentityElement();
            var identityElement = IdentityElement();
            return elements.All(element =>
                elements.Any(candidate =>
                    (Combine(element, candidate) == identityElement) && (Combine(candidate, element) == identityElement)));
        }

        public bool HasInverses() => hasInversesLazy.Value;

        public bool IsGroupOperation() => IsClosed() && IsAssociative() && HasIdentityElement() && HasInverses();
    }
}