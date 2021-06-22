using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class CayleyTable : AggregateRoot
    {
        private const int LettersInAlphabet = 26;

        private readonly Symbol[] symbols;
        private readonly Symbol[][] products;

        public CayleyTable(Guid id, int size) : base(id)
        {
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size), "Cayley table must have at least one element.");

            if (size > LettersInAlphabet)
                throw new ArgumentOutOfRangeException(nameof(size), "Cannot assign symbols to group elements. Cayley table is too large.");

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
