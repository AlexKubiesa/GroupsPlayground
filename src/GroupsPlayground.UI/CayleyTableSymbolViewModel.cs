using GroupsPlayground.Domain;
using System;

namespace GroupsPlayground.UI
{
    public class CayleyTableSymbolViewModel : ViewModel
    {
        public CayleyTableSymbolViewModel(Symbol symbol) => Symbol = symbol;

        public Symbol Symbol { get; }
    }
}
