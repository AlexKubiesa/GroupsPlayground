using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    // TODO: Do we need a Symbol class separate from GroupElement?
    public sealed class Symbol : ValueObject<Symbol>
    {
        public static Symbol Create(string value) => (value == null) ? null : new Symbol(value);

        public Symbol(string value) => Value = value;

        public string Value { get; }

        protected override bool EqualsInternal(Symbol other) => Value.Equals(other.Value);

        protected override int GetHashCodeInternal() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}