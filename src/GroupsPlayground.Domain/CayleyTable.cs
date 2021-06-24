using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
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

        private readonly Symbol[] symbols;
        private readonly Symbol[][] products;

        public CayleyTable(Guid id, int size) : base(id)
        {
            string sizeError = ValidateSize(size);
            if (sizeError != null)
                throw new ArgumentOutOfRangeException(nameof(size), sizeError);

            symbols = new Symbol[size];

            for (int i = 0; i < size; i++)
            {
                string symbol = ((char)('a' + i)).ToString();
                symbols[i] = new Symbol(symbol);
            }

            products = new Symbol[size][];

            for (int i = 0; i < size; i++)
            {
                products[i] = new Symbol[size];
            }

            Size = size;
        }

        public int Size { get; }
        public IReadOnlyList<Symbol> Symbols => symbols;
        public IReadOnlyList<Symbol[]> Products => products;

        public BinaryOperation GetBinaryOperation() =>
            new BinaryOperation(symbols.ToValueList(), Products.Select(x => x.ToValueList()).ToValueList());
    }
}
