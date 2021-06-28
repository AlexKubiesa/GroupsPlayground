using System;
using System.Linq;
using GroupsPlayground.Domain;

namespace GroupsPlayground.Blazor.Pages
{
    public class GroupOperationModel
    {
        public GroupOperationModel(string name, int size)
        {
            Name = name;
            CayleyTable = new CayleyTable(Guid.NewGuid(), size);

            Elements = CayleyTable.Symbols.Select(x => new GroupOperationElementModel(x)).ToArray();

            Products = CayleyTable.Products
                .Select((row, rowIndex) =>
                    row.Select((product, columnIndex) => new GroupOperationProductModel(CayleyTable, rowIndex, columnIndex))
                        .ToArray())
                .ToArray();
        }

        public string Name { get; }
        public CayleyTable CayleyTable { get; }

        public GroupOperationElementModel[] Elements { get; }
        public GroupOperationProductModel[][] Products { get; }
    }

    public class GroupOperationElementModel
    {
        public GroupOperationElementModel(Symbol symbol) => Symbol = symbol?.ToString();

        public string Symbol { get; }
    }

    public class GroupOperationProductModel
    {
        private readonly CayleyTable cayleyTable;
        private readonly int firstIndex;
        private readonly int secondIndex;

        public GroupOperationProductModel(CayleyTable cayleyTable, int firstIndex, int secondIndex)
        {
            this.cayleyTable = cayleyTable ?? throw new ArgumentNullException(nameof(cayleyTable));
            this.firstIndex = firstIndex;
            this.secondIndex = secondIndex;
        }

        public string Symbol
        {
            get => cayleyTable.Products[firstIndex][secondIndex]?.ToString();
            set => cayleyTable.Products[firstIndex][secondIndex] =
                (value == null)
                    ? null
                    : new Symbol(value);
        }
    }
}