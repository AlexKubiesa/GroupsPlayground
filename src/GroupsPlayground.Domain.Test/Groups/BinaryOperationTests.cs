using System.Collections.Generic;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Groups;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace GroupsPlayground.Domain.Test.Groups
{
    public class BinaryOperationTests
    {
        private static readonly Symbol a;
        private static readonly Symbol b;
        private static readonly Symbol c;
        private static readonly BinaryOperation Operation;

        public static IEnumerable<TestCaseData> CombineTestCases { get; }

        static BinaryOperationTests()
        {
            a = new Symbol("a");
            b = new Symbol("b");
            c = new Symbol("c");
            Operation = new BinaryOperation(
                new ValueList<Symbol>(a, b),
                new ValueList<ValueList<Symbol>>(
                    new ValueList<Symbol>(b, a),
                    new ValueList<Symbol>(c, null)));

            CombineTestCases = new[]
            {
                new TestCaseData(a, a, b),
                new TestCaseData(a, b, a),
                new TestCaseData(a, c, null),
                new TestCaseData(a, null, null),
                new TestCaseData(b, a, c),
                new TestCaseData(b, b, null),
                new TestCaseData(b, c, null),
                new TestCaseData(b, null, null),
                new TestCaseData(c, a, null),
                new TestCaseData(c, b, null),
                new TestCaseData(c, c, null),
                new TestCaseData(c, null, null),
                new TestCaseData(null, a, null),
                new TestCaseData(null, b, null),
                new TestCaseData(null, c, null),
                new TestCaseData(null, null, null),
            };
        }

        [Test]
        [TestCaseSource(nameof(CombineTestCases))]
        public void Combine(Symbol first, Symbol second, Symbol expected)
        {
            var actual = Operation.Combine(first, second);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsFullyDefined()
        {
            var operation = new BinaryOperation(
                new ValueList<Symbol>(a, b),
                new ValueList<ValueList<Symbol>>(
                    new ValueList<Symbol>(a, b),
                    new ValueList<Symbol>(b, a)));
            Assert.That(operation.IsFullyDefined);
        }

        [Test]
        public void IsNotFullyDefined()
        {
            var operation = new BinaryOperation(
                new ValueList<Symbol>(a, b),
                new ValueList<ValueList<Symbol>>(
                    new ValueList<Symbol>(a, b),
                    new ValueList<Symbol>(b, null)));
            Assert.That(operation.IsFullyDefined, Is.False);
        }
    }
}