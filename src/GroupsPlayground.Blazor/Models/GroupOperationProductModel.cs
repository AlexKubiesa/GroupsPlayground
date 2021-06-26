using System;
using GroupsPlayground.Domain;

namespace GroupsPlayground.Blazor.Models
{
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