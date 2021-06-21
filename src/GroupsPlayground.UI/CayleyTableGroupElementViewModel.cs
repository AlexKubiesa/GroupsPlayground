using GroupsPlayground.Domain;
using System;

namespace GroupsPlayground.UI
{
    public class CayleyTableGroupElementViewModel : ViewModel
    {
        public CayleyTableGroupElementViewModel(GroupElement groupElement) => GroupElement = groupElement;

        public GroupElement GroupElement { get; }
    }
}
