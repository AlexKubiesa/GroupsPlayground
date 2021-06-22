using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class GroupElement : Entity
    {
        public GroupElement(Guid id, Symbol symbol) : base(id)
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        }

        public Symbol Symbol { get; }
    }
}