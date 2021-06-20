using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Domain
{
    public sealed class CayleyTable : Entity
    {
        private const int LettersInAlphabet = 26;

        private readonly GroupElement[] groupElements;
        private readonly GroupElement[][] products;

        public CayleyTable(Guid id, int size) : base(id)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Cayley table must have at least one element.");
            }

            if (size > LettersInAlphabet)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Cannot assign symbols to group elements. Cayley table is too large.");
            }

            groupElements = new GroupElement[size];

            for (int i = 0; i < size; ++i)
            {
                string symbol = ((char)('a' + i)).ToString();
                groupElements[i] = new GroupElement(symbol);
            }

            products = new GroupElement[size][];

            for (int i = 0; i < size; i++)
            {
                products[i] = new GroupElement[size];
            }

            Size = size;
        }

        public int Size { get; }
        public IReadOnlyList<GroupElement> GroupElements => groupElements;
        public IReadOnlyList<GroupElement[]> Products => products;

        public bool CheckClosure() => Products.SelectMany(x => x).All(GroupElements.Contains);
    }
}
