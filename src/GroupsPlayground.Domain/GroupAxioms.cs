using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public static class GroupAxioms
    {
        public sealed class Compliance : ValueObject<Compliance>
        {
            public Compliance(bool isClosed, bool isAssociative, bool hasIdentity, bool hasInverses)
            {
                IsClosed = isClosed;
                IsAssociative = isAssociative;
                HasIdentity = hasIdentity;
                HasInverses = hasInverses;
            }

            public bool IsClosed { get; }
            public bool IsAssociative { get; }
            public bool HasIdentity { get; }
            public bool HasInverses { get; }
            public bool Success => IsClosed && IsAssociative && HasIdentity && HasInverses;

            protected override bool EqualsInternal(Compliance other) =>
                (IsClosed == other.IsClosed)
                && (IsAssociative == other.IsAssociative)
                && (HasIdentity == other.HasIdentity)
                && (HasInverses == other.HasInverses);

            protected override int GetHashCodeInternal() =>
                HashCode.Combine(IsClosed, IsAssociative, HasIdentity, HasInverses);
        }

        public static Compliance CheckCompliance(BinaryOperation operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            if (!operation.IsFullyDefined())
                throw new ArgumentOutOfRangeException(nameof(operation));

            bool isClosed = IsClosed(operation);
            bool isAssociative = IsAssociative(operation, isClosed);
            var identity = Identity(operation, isClosed);
            bool hasIdentity = (identity != null);
            bool hasInverses = HasInverses(operation, identity);
            return new Compliance(isClosed, isAssociative, hasIdentity, hasInverses);
        }

        private static bool IsClosed(BinaryOperation operation) =>
            operation.Range.All(operation.Domain.Contains);

        private static bool IsAssociative(BinaryOperation operation, bool isClosed) =>
            isClosed
            && operation.Domain
                .SelectMany(first =>
                    operation.Domain.SelectMany(second =>
                        operation.Domain.Select(third => (first, second, third))))
                .All(x =>
                    operation.Combine(operation.Combine(x.first, x.second), x.third)
                    == operation.Combine(x.first, operation.Combine(x.second, x.third)));

        private static Symbol Identity(BinaryOperation operation, bool isClosed) =>
            operation.Domain.FirstOrDefault(candidate =>
                operation.Domain.All(other =>
                    (operation.Combine(candidate, other) == other) && (operation.Combine(other, candidate) == other)));

        private static bool HasInverses(BinaryOperation operation, Symbol identity) =>
            (identity != null)
            && operation.Domain.All(element => 
                operation.Domain.Any(candidate => 
                    (operation.Combine(element, candidate) == identity) 
                    && (operation.Combine(candidate, element) == identity)));
    }
}