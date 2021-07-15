using System.Collections.Generic;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Groups;
using NUnit.Framework;

namespace GroupsPlayground.Domain.Test
{
    [TestFixture]
    public class GroupAxiomComplianceTests
    {
        private static readonly Symbol a;
        private static readonly Symbol b;
        private static readonly Symbol c;
        private static readonly BinaryOperation Operation;

        public static IEnumerable<TestCaseData> CheckComplianceTestCases { get; }

        static GroupAxiomComplianceTests()
        {
            a = new Symbol("a");
            b = new Symbol("b");
            c = new Symbol("c");
            Operation = new BinaryOperation(
                new ValueList<Symbol>(a, b),
                new ValueList<ValueList<Symbol>>(
                    new ValueList<Symbol>(b, a),
                    new ValueList<Symbol>(c, null)));

            CheckComplianceTestCases = new[]
            {
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a),
                        new ValueList<ValueList<Symbol>>(
                            new ValueList<Symbol>(a))), 
                        new GroupAxioms.Compliance(
                            isClosed: true,
                            isAssociative: true,
                            hasIdentity: true,
                            hasInverses: true))
                    .SetName("One element group"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(b))),
                        new GroupAxioms.Compliance(
                            isClosed: false,
                            isAssociative: false,
                            hasIdentity: false,
                            hasInverses: false))
                    .SetName("One element, not closed"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a, b),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(a, b),
                                new ValueList<Symbol>(b, a))),
                        new GroupAxioms.Compliance(
                            isClosed: true,
                            isAssociative: true,
                            hasIdentity: true,
                            hasInverses: true))
                    .SetName("Two element group"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a, b),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(a, b),
                                new ValueList<Symbol>(b, c))),
                        new GroupAxioms.Compliance(
                            isClosed: false,
                            isAssociative: false,
                            hasIdentity: true,
                            hasInverses: false))
                    .SetName("Two elements, not closed, has identity"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a, b),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(a, c),
                                new ValueList<Symbol>(c, c))),
                        new GroupAxioms.Compliance(
                            isClosed: false,
                            isAssociative: false,
                            hasIdentity: false,
                            hasInverses: false))
                    .SetName("Two elements, not closed, no identity"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a, b),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(a, a),
                                new ValueList<Symbol>(a, a))),
                        new GroupAxioms.Compliance(
                            isClosed: true,
                            isAssociative: true,
                            hasIdentity: false,
                            hasInverses: false))
                    .SetName("Two elements, associative, no identity"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a, b),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(b, b),
                                new ValueList<Symbol>(a, a))),
                        new GroupAxioms.Compliance(
                            isClosed: true,
                            isAssociative: false,
                            hasIdentity: false,
                            hasInverses: false))
                    .SetName("Two elements, not associative"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a, b),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(a, b),
                                new ValueList<Symbol>(b, b))),
                        new GroupAxioms.Compliance(
                            isClosed: true,
                            isAssociative: true,
                            hasIdentity: true,
                            hasInverses: false))
                    .SetName("Two elements, closed, has identity, no inverses"),
                new TestCaseData(
                        new BinaryOperation(
                            new ValueList<Symbol>(a, b, c),
                            new ValueList<ValueList<Symbol>>(
                                new ValueList<Symbol>(a, b, c),
                                new ValueList<Symbol>(b, c, c),
                                new ValueList<Symbol>(c, b, b))),
                                new GroupAxioms.Compliance(
                            isClosed: true,
                            isAssociative: false,
                            hasIdentity: true,
                            hasInverses: false))
                    .SetName("Three elements, not associative, has identity"),
            };
        }

        [Test]
        [TestCaseSource(nameof(CheckComplianceTestCases))]
        public void CheckCompliance(BinaryOperation operation, GroupAxioms.Compliance expected)
        {
            var actual = GroupAxioms.CheckCompliance(operation);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}