using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Groups
{
    public sealed class CayleyTable : AggregateRoot
    {
        private const int LettersInAlphabet = 26;

        public static string ValidateSize(int size) =>
            size switch
            {
                var small when small < 1 => "Cayley table must have at least one element.",
                var large when large > LettersInAlphabet => "Cayley table is too large to assign symbols to group elements.",
                _ => null
            };

        private readonly GroupElementSymbols symbols;
        private readonly Symbol[][] products;

        public CayleyTable(Guid id, GroupElementSymbols symbols) : base(id)
        {
            this.symbols = symbols;

            Size = symbols.Count;

            products = new Symbol[Size][];

            for (int i = 0; i < Size; i++)
            {
                products[i] = new Symbol[Size];
            }

        }

        public int Size { get; }
        public IReadOnlyList<Symbol> Symbols => symbols;
        public IReadOnlyList<Symbol[]> Products => products;

        public BinaryOperation GetBinaryOperation() =>
            new BinaryOperation(symbols.ToValueList(), Products.Select(x => ValueListExtensions.ToValueList<Symbol>(x)).ToValueList());
    }
}
