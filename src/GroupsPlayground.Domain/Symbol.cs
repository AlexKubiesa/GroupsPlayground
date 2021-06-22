using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Symbol : ValueObject<Symbol>
    {
        private static bool IsAllowedCharacter(char c) => char.IsLetterOrDigit(c) || char.IsSymbol(c);

        private readonly string text;

        public Symbol(string text)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));

            if (!text.All(IsAllowedCharacter))
                throw new ArgumentOutOfRangeException(nameof(text), "Symbol contains invalid characters.");
        }

        protected override bool EqualsInternal(Symbol other) => text.Equals(other.text, StringComparison.Ordinal);

        protected override int GetHashCodeInternal() => text.GetHashCode(StringComparison.Ordinal);

        public override string ToString() => text;
    }
}