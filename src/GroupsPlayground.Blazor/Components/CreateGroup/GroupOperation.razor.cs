using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Domain;
using GroupsPlayground.Domain.Groups;

namespace GroupsPlayground.Blazor.Components.CreateGroup
{
    public class GroupOperationModel
    {
        private GroupElementSymbols symbols;
        private readonly List<GroupOperationElementModel> elements = new();

        public GroupElementSymbols Symbols
        {
            get => symbols;
            set
            {
                if (value == null)
                    return;

                symbols = value;

                CayleyTable = new CayleyTable(Guid.NewGuid(), value);

                elements.Clear();
                elements.AddRange(CayleyTable.Symbols.Select(x => new GroupOperationElementModel(x)));

                Products = CayleyTable.Products
                    .Select((row, rowIndex) =>
                        row.Select((product, columnIndex) => new GroupOperationProductModel(CayleyTable, rowIndex, columnIndex))
                            .ToArray())
                    .ToArray();
            }
        }

        public CayleyTable CayleyTable { get; private set; }
        public IReadOnlyList<GroupOperationElementModel> Elements => elements;
        public GroupOperationProductModel[][] Products { get; private set; }
        public string ValidationMessage { get; set; }
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
