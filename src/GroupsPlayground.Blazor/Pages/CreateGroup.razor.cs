using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Domain;

namespace GroupsPlayground.Blazor.Pages
{
    public class CreateGroupModel
    {
    }

    public class GroupOperationModel
    {
        public GroupOperationModel(int elementCount)
        {
            CayleyTable = new CayleyTable(Guid.NewGuid(), elementCount);

            Elements = CayleyTable.Symbols.Select(x => new GroupOperationElementModel(x)).ToArray();

            Products = CayleyTable.Products
                .Select((row, rowIndex) =>
                    row.Select((product, columnIndex) => new GroupOperationProductModel(CayleyTable, rowIndex, columnIndex))
                        .ToArray())
                .ToArray();
        }

        public CayleyTable CayleyTable { get; }
        public GroupOperationElementModel[] Elements { get; }
        public GroupOperationProductModel[][] Products { get; }
        public string ValidationMessage { get; set; }
        public bool Visible { get; set; }
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
