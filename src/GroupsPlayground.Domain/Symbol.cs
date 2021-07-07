﻿using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Symbol : ValueObject<Symbol>
    {
        public static Symbol Create(string text) => string.IsNullOrEmpty(text) ? null : new Symbol(text);

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