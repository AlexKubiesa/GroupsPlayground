using System;
using System.Collections.Generic;

namespace GroupsPlayground.Domain.Framework
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

        protected AggregateRoot(Guid id) : base(id)
        {
        }

        public IReadOnlyList<IDomainEvent> DomainEvents => domainEvents;

        protected void AddDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => domainEvents.Clear();
    }
}