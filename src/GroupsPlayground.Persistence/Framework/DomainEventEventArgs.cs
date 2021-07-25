using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Framework
{
    public sealed class DomainEventEventArgs : EventArgs
    {
        public DomainEventEventArgs(IDomainEvent domainEvent)
        {
            DomainEvent = domainEvent ?? throw new ArgumentNullException(nameof(domainEvent));
        }

        public IDomainEvent DomainEvent { get; }
    }
}