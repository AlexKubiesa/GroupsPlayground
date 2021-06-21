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

        public string Product
        {
            get => cayleyTable.Products[firstIndex][secondIndex]?.Symbol;
            set
            {
                var productElement =
                    (string.IsNullOrWhiteSpace(value))
                        ? null
                        : (cayleyTable.GroupElements.SingleOrDefault(x => x.Symbol == value) 
                           ?? new GroupElement(Guid.NewGuid(), value));
                cayleyTable.Products[firstIndex][secondIndex] = productElement;
                Notify();
            }
        }
    }
}
