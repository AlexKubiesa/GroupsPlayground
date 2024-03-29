﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Groups.Internal;
using PermutationGrammarLexer = GroupsPlayground.Domain.Groups.Internal.PermutationGrammarLexer;
using PermutationGrammarParser = GroupsPlayground.Domain.Groups.Internal.PermutationGrammarParser;

namespace GroupsPlayground.Domain.Groups
{
    public sealed class Permutation : ValueObject<Permutation>
    {
        private readonly ValueList<Cycle> cycles;

        public static Permutation Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return null;

            var reader = new StringReader(expression);
            var stream = new AntlrInputStream(reader);
            var lexer = new PermutationGrammarLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new PermutationGrammarParser(tokens);
            var context = parser.permutation();

            if (parser.NumberOfSyntaxErrors > 0)
                throw new ValidationError("Invalid permutation.");

            return PermutationEvaluator.Evaluate(context);
        }

        private static bool ArePairwiseDisjoint(IEnumerable<Cycle> cycles)
        {
            var elements = new HashSet<int>();
            return cycles.All(cycle => cycle.All(elements.Add));
        }

        public Permutation(ValueList<Cycle> cycles)
        {
            if (cycles == null)
                throw new ArgumentNullException(nameof(cycles));

            if (!ArePairwiseDisjoint(cycles))
                throw new ArgumentException("Cycles must be pairwise disjoint.", nameof(cycles));

            this.cycles = cycles.Where(x => x.Count > 1).ToValueList();
        }

        protected override bool EqualsInternal(Permutation other) => cycles.Equals(other.cycles);

        protected override int GetHashCodeInternal() => cycles.GetHashCode();

        public override string ToString() =>
            cycles.Any()
                ? string.Concat(cycles.Select(x => x.ToString()))
                : "()";

        public int Map(int element)
        {
            var cycle = cycles.FirstOrDefault(x => x.Contains(element));
            return cycle?.Map(element) ?? element;
        }

        public Permutation Multiply(Permutation other)
        {
            var newCycles = new List<Cycle>();

            var elements = new SortedSet<int>();

            foreach (var cycle in cycles)
                elements.UnionWith(cycle);

            foreach (var cycle in other.cycles)
                elements.UnionWith(cycle);

            while (elements.Any())
            {
                var newCycle = new List<int>();
                int newElement = elements.Min;

                while (!newCycle.Contains(newElement))
                {
                    elements.Remove(newElement);
                    newCycle.Add(newElement);
                    newElement = Map(other.Map(newElement));
                }

                newCycles.Add(new Cycle(newCycle));
            }

            return new Permutation(newCycles.ToValueList());
        }
    }
}