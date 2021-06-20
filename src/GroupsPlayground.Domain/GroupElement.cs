using System;

namespace GroupsPlayground.Domain
{
    public sealed class GroupElement : ValueObject<GroupElement>
    {
        public GroupElement(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentException($"'{nameof(symbol)}' cannot be null or whitespace", nameof(symbol));
            }

            Symbol = symbol;
        }

        public string Symbol { get; }

        protected override bool EqualsInternal(GroupElement other) => Equals(Symbol, other.Symbol);

        protected override int GetHashCodeInternal() => Symbol?.GetHashCode() ?? 0;
    }
}
