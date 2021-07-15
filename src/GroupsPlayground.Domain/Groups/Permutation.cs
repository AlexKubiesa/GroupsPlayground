using System;
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

        public Permutation(ValueList<Cycle> cycles)
        {
            this.cycles = cycles ?? throw new ArgumentNullException(nameof(cycles));
        }

        protected override bool EqualsInternal(Permutation other) => cycles.Equals(other.cycles);

        protected override int GetHashCodeInternal() => cycles.GetHashCode();

        public override string ToString() => '(' + string.Concat(cycles.Select(x => x.ToString())) + ')';
    }
}