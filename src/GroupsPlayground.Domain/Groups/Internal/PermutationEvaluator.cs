﻿using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Groups.Internal
{
    internal static class PermutationEvaluator
    {
        public static Permutation Evaluate(PermutationGrammarParser.PermutationContext permutationContext) =>
            new Permutation(permutationContext.cycle().Select(Evaluate).ToValueList());

        public static Cycle Evaluate(PermutationGrammarParser.CycleContext cycleContext) =>
            new Cycle(cycleContext.number().Select(Evaluate).ToList());

        public static int Evaluate(PermutationGrammarParser.NumberContext numberContext) =>
            int.Parse(numberContext.GetText());
    }
}