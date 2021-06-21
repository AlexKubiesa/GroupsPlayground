using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class GroupElement : Entity
    {
        public GroupElement(Guid id, string symbol) : base(id)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(symbol));
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}