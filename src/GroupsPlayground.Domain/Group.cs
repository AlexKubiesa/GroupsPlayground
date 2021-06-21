using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Group : Entity
    {
        private readonly PartialBinaryOperation operation;

        public Group(Guid id, CayleyTable cayleyTable) : base(id)
        {
            if (cayleyTable == null)
                throw new ArgumentNullException(nameof(cayleyTable));

            operation = cayleyTable.CreatePartialBinaryOperation();

            if (!operation.IsGroupOperation())
                throw new ArgumentOutOfRangeException(nameof(cayleyTable),
                    "The operation defined by the Cayley table is not a group operation.");
        }

        public Group(Guid id) : base(id)
        {
        }
    }
}