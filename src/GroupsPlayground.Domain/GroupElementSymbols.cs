using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class GroupElementSymbols : ValueObject<GroupElementSymbols>, IReadOnlyList<Symbol>
    {
        public static string Validate(ValueList<Symbol> symbols)
        {
            if (symbols.Any(x => x == null))
                return "Some symbols are missing.";

            if (!symbols.IsDistinct())
                return "The symbols are not distinct.";

            return null;
        }

        private readonly ValueList<Symbol> symbols;

        public GroupElementSymbols(ValueList<Symbol> symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException(nameof(symbols));

            string error = Validate(symbols);
            if (error != null)
                throw new ArgumentOutOfRangeException(nameof(symbols), error);

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