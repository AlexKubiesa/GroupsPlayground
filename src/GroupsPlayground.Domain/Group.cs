using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Group : AggregateRoot
    {
        private Group(Guid id) : base(id)
        {
        }

        public Group(Guid id, CayleyTable cayleyTable) : this(id)
        {
            if (cayleyTable == null)
                throw new ArgumentNullException(nameof(cayleyTable));

            var operation = cayleyTable.GetOperation();
            var compliance = GroupAxioms.CheckCompliance(operation);

            if (!compliance.Success)
                throw new ArgumentOutOfRangeException(nameof(cayleyTable),
                    "The operation defined by the Cayley table is not a group operation.");

            Elements = cayleyTable.Symbols.Select(x => new GroupElement(Guid.NewGuid(), x)).ToArray();
            Products = cayleyTable.Products
                .SelectMany((row, rowIndex) =>
                    row.Select((product, columnIndex) =>
                            new GroupElementProduct(
                                Guid.NewGuid(),
                                Elements[rowIndex],
                                Elements[columnIndex],
                                Elements.Single(x => x.Symbol.Equals(product))))
                        .ToList())
                .ToList();
        }

        public string Name { get; set; }

        public IReadOnlyList<GroupElement> Elements { get; }

        public IReadOnlyCollection<GroupElementProduct> Products { get; }
    }
}