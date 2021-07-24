using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Quests
{
    public sealed class Quest : AggregateRoot
    {
        public Quest(Guid id, bool complete, string description) : base(id)
        {
            Complete = complete;
            Description = description;
        }

        public bool Complete { get; set; }
        public string Description { get; }
    }
}