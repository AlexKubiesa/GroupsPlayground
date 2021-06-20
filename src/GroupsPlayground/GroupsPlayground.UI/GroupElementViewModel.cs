using GroupsPlayground.Domain;
using System;

namespace GroupsPlayground.UI
{
    public class GroupElementViewModel : ViewModel
    {
        private readonly GroupElement groupElement;

        public GroupElementViewModel(GroupElement groupElement)
        {
            this.groupElement = groupElement ?? throw new ArgumentNullException(nameof(groupElement));
        }

        public string GroupElementSymbol => groupElement.Symbol;
    }
}
