using System;

namespace GroupsPlayground.Domain
{
    public sealed class GroupElement : Entity
    {
        /// <remarks>
        /// This constructor is internal to prevent group elements from being constructed outside a group or Cayley table.
        /// </remarks>
        internal GroupElement(Guid id, string symbol) : base(id)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentException($"'{nameof(symbol)}' cannot be null or whitespace", nameof(symbol));
            }

            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}
