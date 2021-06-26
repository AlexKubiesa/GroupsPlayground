using System;
using System.Linq;
using GroupsPlayground.Domain;

namespace GroupsPlayground.Blazor.Models
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
}