using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Framework
{
    public sealed class AggregateEventArgs : EventArgs
    {
        public AggregateEventArgs(AggregateRoot aggregateRoot)
        {
            AggregateRoot = aggregateRoot ?? throw new ArgumentNullException(nameof(aggregateRoot));
        }

        public AggregateRoot AggregateRoot { get; }
    }
}