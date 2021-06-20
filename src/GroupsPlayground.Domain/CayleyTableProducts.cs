using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupsPlayground.Domain
{
    public sealed class CayleyTableProducts : IEnumerable<GroupElement>
    {
        private readonly GroupElement[] groupElements;
        private readonly GroupElement[][] products;

        internal CayleyTableProducts(GroupElement[] groupElements)
        {
            this.groupElements = groupElements ?? throw new ArgumentNullException(nameof(groupElements));

            products = new GroupElement[groupElements.Length][];

            for (int i = 0; i < groupElements.Length; i++)
            {
                products[i] = new GroupElement[groupElements.Length];
            }
        }

        public GroupElement this[int firstIndex, int secondIndex]
        {
            get => products[firstIndex][secondIndex];
            set => products[firstIndex][secondIndex] = value;
        }

        public GroupElement this[GroupElement first, GroupElement second]
        {
            get => this[Array.IndexOf(groupElements, first), Array.IndexOf(groupElements, second)];
            set => this[Array.IndexOf(groupElements, first), Array.IndexOf(groupElements, second)] = value;
        }

        public IReadOnlyList<IReadOnlyList<GroupElement>> Rows => products;

        public IEnumerator<GroupElement> GetEnumerator() => products.SelectMany(x => x).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
