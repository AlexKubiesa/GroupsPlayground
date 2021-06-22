using System;

namespace GroupsPlayground.Domain.Framework
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id) : base(id)
        {
        }
    }
}