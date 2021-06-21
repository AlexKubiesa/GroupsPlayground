using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Group : Entity
    {
        // TODO: How to ensure Groups are always valid?
        private readonly PartialBinaryOperation operation;

        public Group(Guid id, CayleyTable cayleyTable) : base(id)
        {
            if (cayleyTable == null)
                throw new ArgumentNullException(nameof(cayleyTable));

            operation = cayleyTable.CreatePartialBinaryOperation();

            if (!operation.IsGroupOperation())
                throw new ArgumentOutOfRangeException(nameof(cayleyTable),
                    "The operation defined by the Cayley table is not a group operation.");

            Elements = cayleyTable.Symbols.Select(x => new GroupElement(Guid.NewGuid(), x.Value)).ToList();
            Products = cayleyTable.Products
                .SelectMany((row, rowIndex) =>
                    row.Select((product, columnIndex) =>
                            new GroupElementProduct(Guid.NewGuid())
                            {
                                First = Elements[rowIndex],
                                Second = Elements[columnIndex],
                                Product = Elements.Single(x => x.Symbol == product.Value)
                            })
                        .ToList())
                .ToList();
        }

        public Group(Guid id) : base(id)
        {
        }

        public IList<GroupElement> Elements { get; }

        public ICollection<GroupElementProduct> Products { get; }
    }
}