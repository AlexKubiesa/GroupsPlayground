using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Group : Entity
    {
        private Group(Guid id) : base(id)
        {
        }

        public Group(Guid id, CayleyTable cayleyTable) : this(id)
        {
            if (cayleyTable == null)
                throw new ArgumentNullException(nameof(cayleyTable));

            var operation = cayleyTable.CreatePartialBinaryOperation();

            if (!operation.IsGroupOperation())
                throw new ArgumentOutOfRangeException(nameof(cayleyTable),
                    "The operation defined by the Cayley table is not a group operation.");

            Elements = cayleyTable.Symbols.Select(x => new GroupElement(Guid.NewGuid(), x.Value)).ToList();
            Products = cayleyTable.Products
                .SelectMany((row, rowIndex) =>
                    row.Select((product, columnIndex) =>
                            new GroupElementProduct(
                                Guid.NewGuid(),
                                Elements[rowIndex],
                                Elements[columnIndex],
                                Elements.Single(x => x.Symbol == product.Value)))
                        .ToList())
                .ToList();
        }

        public IReadOnlyList<GroupElement> Elements { get; }

        public IReadOnlyCollection<GroupElementProduct> Products { get; }
    }
}