using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Group : AggregateRoot
    {
        public static string ValidateName(string name) =>
            name switch
            {
                { } valid when valid.All(char.IsLetterOrDigit) => null,
                var missing when string.IsNullOrEmpty(missing) => "Missing group name.",
                _ => "Invalid characters in group name."
            };

        private string name;

        private Group(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public Group(Guid id, string name, CayleyTable cayleyTable) : this(id, name)
        {
            if (cayleyTable == null)
                throw new ArgumentNullException(nameof(cayleyTable));

            var operation = cayleyTable.GetBinaryOperation();
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

        public string Name
        {
            get => name;
            set
            {
                string error = ValidateName(value);
                if (error != null)
                    throw new ArgumentOutOfRangeException(nameof(value), error);
                name = value;
            }
        }

        public IReadOnlyList<GroupElement> Elements { get; }

        public IReadOnlyCollection<GroupElementProduct> Products { get; }
    }
}