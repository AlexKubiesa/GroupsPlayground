using GroupsPlayground.Domain;
using System;

namespace GroupsPlayground.UI
{
    public class CayleyTableProductViewModel : ViewModel
    {
        private readonly CayleyTable cayleyTable;
        private readonly int firstIndex;
        private readonly int secondIndex;

        public CayleyTableProductViewModel(CayleyTable cayleyTable, int firstIndex, int secondIndex)
        {
            this.cayleyTable = cayleyTable ?? throw new ArgumentNullException(nameof(cayleyTable));
            this.firstIndex = firstIndex;
            this.secondIndex = secondIndex;
        }

        public string GroupElementSymbol
        {
            get => cayleyTable.Products[firstIndex, secondIndex]?.Symbol;
            set
            {
                cayleyTable.Products[firstIndex, secondIndex] = (value == null) ? null : new GroupElement(value);
                Notify();
            }
        }
    }
}
