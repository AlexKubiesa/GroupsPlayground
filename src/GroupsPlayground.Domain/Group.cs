﻿using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Group : AggregateRoot
    {
        public static string ValidateName(string name) =>
            name switch
            {
                { } valid when valid.All(c => char.IsLetterOrDigit(c) || c == '_') => null,
                var missing when string.IsNullOrEmpty(missing) => "Missing group name.",
                _ => "Invalid characters in group name."
            };

        private string name;
        private readonly IReadOnlyList<GroupElement> elements;
        private readonly IDictionary<GroupElement, IDictionary<GroupElement, GroupElement>> products;

        private Group(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public Group(Guid id, string name, CayleyTable cayleyTable) : this(id, name)
        {
            if (cayleyTable == null)
                throw new ArgumentNullException(nameof(cayleyTable));

            var operation = cayleyTable.GetBinaryOperation();
            var compliance = GroupAxioms.CheckCompliance(operation);

            if (!compliance.Success)
                throw new ArgumentOutOfRangeException(nameof(cayleyTable),
                    "The operation defined by the Cayley table is not a group operation.");

            elements = cayleyTable.Symbols.Select(x => new GroupElement(Guid.NewGuid(), x)).ToArray();

            products = new Dictionary<GroupElement, IDictionary<GroupElement, GroupElement>>(elements.Count);
            for (int firstIndex = 0; firstIndex < elements.Count; firstIndex++)
            {
                var first = elements[firstIndex];
                products.Add(first, new Dictionary<GroupElement, GroupElement>(elements.Count));
                for (int secondIndex = 0; secondIndex < elements.Count; secondIndex++)
                {
                    var second = elements[secondIndex];
                    products[first][second] =
                        elements.Single(x => x.Symbol.Equals(cayleyTable.Products[firstIndex][secondIndex]));
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                string error = ValidateName(value);
                if (error != null)
                    throw new ArgumentOutOfRangeException(nameof(value), error);
                name = value;
            }
        }

        public int Size => Elements.Count;

        public IReadOnlyList<IGroupElement> Elements => elements;

        public void RenameElement(IGroupElement element, Symbol newSymbol)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            if (newSymbol == null)
                throw new ArgumentNullException(nameof(newSymbol));
            if (!elements.Contains(element))
                throw new InvalidOperationException("The group does not contain this element.");
            if (elements.Except(new[] { element }).Any(x => x.Symbol == newSymbol))
                throw new InvalidOperationException($"Symbol {newSymbol} is already in use.");
            ((GroupElement)element).Symbol = newSymbol;
        }

        public IGroupElement Multiply(IGroupElement first, IGroupElement second) => products[(GroupElement)first][(GroupElement)second];
    }
}