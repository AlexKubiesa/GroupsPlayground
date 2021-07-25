using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Framework
{
    public abstract class Repository
    {
        internal event EventHandler<AggregateEventArgs> AggregateAdded;
        internal event EventHandler<AggregateEventArgs> AggregateUpdated;
        internal event EventHandler<AggregateEventArgs> AggregateRemoved;
        internal event EventHandler<DomainEventEventArgs> DomainEventRaised;

        protected void OnAggregateAdded(AggregateRoot aggregateRoot) =>
            AggregateAdded?.Invoke(this, new AggregateEventArgs(aggregateRoot));

        protected void OnAggregateUpdated(AggregateRoot aggregateRoot) =>
            AggregateUpdated?.Invoke(this, new AggregateEventArgs(aggregateRoot));

        protected void OnAggregateRemoved(AggregateRoot aggregateRoot) =>
            AggregateRemoved?.Invoke(this, new AggregateEventArgs(aggregateRoot));

        protected void OnDomainEventRaised(IDomainEvent domainEvent) =>
            DomainEventRaised?.Invoke(this, new DomainEventEventArgs(domainEvent));
    }
}