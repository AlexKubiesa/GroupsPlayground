using System;
using System.Collections.Generic;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Persistence.Framework;

namespace GroupsPlayground.Persistence.Common
{
    public abstract class AppSession : IDisposable
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

        protected void AddRepository(Repository repository)
        {
            repository.AggregateAdded += RepositoryAggregateAdded;
            repository.AggregateUpdated += RepositoryAggregateUpdated;
            repository.DomainEventRaised += RepositoryDomainEventRaised;
        }

        private protected AppDbContext Context { get; } = new AppDbContext();

        public void Dispose() => Context?.Dispose();

        private void RepositoryAggregateAdded(object sender, AggregateEventArgs eventArgs) =>
            QueueDomainEvents(eventArgs.AggregateRoot);

        private void RepositoryAggregateUpdated(object sender, AggregateEventArgs eventArgs) =>
            QueueDomainEvents(eventArgs.AggregateRoot);

        private void RepositoryDomainEventRaised(object sender, DomainEventEventArgs eventArgs) =>
            QueueDomainEvent(eventArgs.DomainEvent);

        private void QueueDomainEvents(AggregateRoot aggregateRoot)
        {
            domainEvents.AddRange(aggregateRoot.DomainEvents);
            aggregateRoot.ClearDomainEvents();
        }

        private void QueueDomainEvent(IDomainEvent domainEvent) => domainEvents.Add(domainEvent);

        public void SaveChanges()
        {
            Context.SaveChanges();
            DispatchEvents();
        }

        private void DispatchEvents()
        {
            DomainEvents.Dispatch(domainEvents);
            domainEvents.Clear();
        }
    }
}