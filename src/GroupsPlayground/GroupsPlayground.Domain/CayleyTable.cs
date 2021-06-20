using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Domain
{
    public sealed class CayleyTable : Entity
    {
        private const int LettersInAlphabet = 26;

        private readonly GroupElement[] groupElements;
        private readonly CayleyTableProducts products;

        public CayleyTable(Guid id, int size) : base(id)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size), "Cayley table must have at least one element.");

            if (size > LettersInAlphabet)
                throw new ArgumentOutOfRangeException(nameof(size), "Cannot assign symbols to group elements. Cayley table is too large.");

            groupElements = new GroupElement[size];

            for (int i = 0; i < size; i++)
            {
                string symbol = ((char)('a' + i)).ToString();
                groupElements[i] = new GroupElement(symbol);
            }

            products = new CayleyTableProducts(groupElements);

            Size = size;
        }

        public int Size { get; }
        public IReadOnlyList<GroupElement> GroupElements => groupElements;
        public CayleyTableProducts Products => products;

        public bool CheckFullyDefined() => Products.All(x => x != null);

        public bool CheckClosure() => Products.All(GroupElements.Contains);

        public bool CheckAssociativity() =>
            CheckFullyDefined() &&
            CheckClosure() &&
            GroupElements
            .SelectMany(first => GroupElements.SelectMany(second => GroupElements.Select(third => (first, second, third))))
            .All(x => Products[Products[x.first, x.second], x.third] == Products[x.first, Products[x.second, x.third]]);

        public GroupElement IdentityElement() =>
            GroupElements.FirstOrDefault(candidate =>
                GroupElements.All(other =>
                    (Products[candidate, other] == other) && (Products[other, candidate] == other)));

        public bool CheckIdentityElement() => IdentityElement() != null;

        public bool CheckInverses()
        {
            var identityElement = IdentityElement();
            if (identityElement == null)
                return false;
            return GroupElements.All(element =>
                GroupElements.Any(candidate =>
                    (Products[element, candidate] == identityElement) && (Products[candidate, element] == identityElement)));
        }
    }
}
