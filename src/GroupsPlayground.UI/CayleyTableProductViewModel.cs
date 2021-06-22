using GroupsPlayground.Domain;
using System;
using System.Linq;

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

        public Symbol Product
        {
            get => cayleyTable.Products[firstIndex][secondIndex];
            set
            {
                cayleyTable.Products[firstIndex][secondIndex] = value;
                Notify();
            }
        }
    }
}
