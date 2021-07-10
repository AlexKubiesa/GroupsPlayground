using System;
using System.Linq;

namespace GroupsPlayground.Persistence.Model
{
    public sealed class Symbol : ValueObject<Symbol>
    {
        private static bool IsAllowedCharacter(char c) => char.IsLetterOrDigit(c) || char.IsSymbol(c);

        public Symbol(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));

            if (!text.All(IsAllowedCharacter))
                throw new ArgumentOutOfRangeException(nameof(text), "Symbol contains invalid characters.");
        }

        public string Text { get; }

        protected override bool EqualsInternal(Symbol other) => Text.Equals(other.Text, StringComparison.Ordinal);

        protected override int GetHashCodeInternal() => Text.GetHashCode(StringComparison.Ordinal);

        public override string ToString() => Text;
    }
}