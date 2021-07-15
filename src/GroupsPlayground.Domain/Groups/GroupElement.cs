using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Groups
{
    internal sealed class GroupElement : Entity, IGroupElement
    {
        private Symbol symbol;

        public GroupElement(Guid id, Symbol symbol) : base(id)
        {
            Symbol = symbol;
        }

        public Symbol Symbol
        {
            get => symbol;
            set => symbol = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}