using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class GroupElementSymbols : ValueObject<GroupElementSymbols>, IReadOnlyList<Symbol>
    {
        private readonly ValueList<Symbol> symbols;

        public GroupElementSymbols(ValueList<Symbol> symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException(nameof(symbols));

            if (symbols.Any(x => x == null))
                throw new ValidationError("Some symbols are missing.");

            if (!symbols.AreDistinct())
                throw new ValidationError("The symbols are not distinct.");

            this.symbols = symbols;
        }

        protected override bool EqualsInternal(GroupElementSymbols other) => symbols.Equals(other.symbols);

        protected override int GetHashCodeInternal() => symbols.GetHashCode();

        public IEnumerator<Symbol> GetEnumerator() => symbols.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => symbols.Count;

        public Symbol this[int index] => symbols[index];
    }
}