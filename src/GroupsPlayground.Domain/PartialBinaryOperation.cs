using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class PartialBinaryOperation : ValueObject<PartialBinaryOperation>
    {
        private readonly ValueList<GroupElement> groupElements;
        private readonly ValueList<ValueList<GroupElement>> products;
        private readonly Lazy<bool> isFullyDefinedLazy;
        private readonly Lazy<bool> isClosedLazy;
        private readonly Lazy<bool> isAssociativeLazy;
        private readonly Lazy<GroupElement> identityElementLazy;
        private readonly Lazy<bool> hasInversesLazy;

        public PartialBinaryOperation(ValueList<GroupElement> groupElements, ValueList<ValueList<GroupElement>> products)
        {
            this.groupElements = groupElements ?? throw new ArgumentNullException(nameof(groupElements));
            this.products = products ?? throw new ArgumentNullException(nameof(products));

            isFullyDefinedLazy = new Lazy<bool>(IsFullyDefinedImpl);
            isClosedLazy = new Lazy<bool>(IsClosedImpl);
            isAssociativeLazy = new Lazy<bool>(IsAssociativeImpl);
            identityElementLazy = new Lazy<GroupElement>(IdentityElementImpl);
            hasInversesLazy = new Lazy<bool>(HasInversesImpl);
        }

        protected override bool EqualsInternal(PartialBinaryOperation other) =>
            groupElements.Equals(other.groupElements) && products.Equals(other.products);

        protected override int GetHashCodeInternal() =>
            HashCode.Combine(groupElements.GetHashCode(), products.GetHashCode());

        public GroupElement Combine(GroupElement first, GroupElement second)
        {
            if (first == null || second == null)
                return null;

            int firstIndex = groupElements.IndexOf(first);
            if (firstIndex < 0)
                return null;

            int secondIndex = groupElements.IndexOf(second);
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
            return products.SelectMany(x => x).All(groupElements.Contains);
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
            return groupElements
                .SelectMany(first => groupElements.SelectMany(second => groupElements.Select(third => (first, second, third))))
                .All(x => Combine(Combine(x.first, x.second), x.third) == Combine(x.first, Combine(x.second, x.third)));
        }

        public bool IsAssociative() => isAssociativeLazy.Value;

        private GroupElement IdentityElementImpl()
        {
            EnsureFullyDefined();
            EnsureClosed();
            return groupElements.FirstOrDefault(candidate =>
                groupElements.All(other =>
                    (Combine(candidate, other) == other) && (Combine(other, candidate) == other)));
        }

        public GroupElement IdentityElement() => identityElementLazy.Value;

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
            return groupElements.All(element =>
                groupElements.Any(candidate =>
                    (Combine(element, candidate) == identityElement) && (Combine(candidate, element) == identityElement)));
        }

        public bool HasInverses() => hasInversesLazy.Value;

        public bool IsGroupOperation() => IsClosed() && IsAssociative() && HasIdentityElement() && HasInverses();
    }
}