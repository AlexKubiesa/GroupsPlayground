using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Quests
{
    public abstract class Quest : AggregateRoot
    {
        private static readonly Guid CreateGroupId = Guid.Parse("C5DA5525-2A22-4F1D-A8C6-1458A9D632F9");
        
        public static Quest Create(Guid id, bool complete, string description)
        {
            if (id == CreateGroupId)
                return new CreateGroupQuest(id, complete, description);
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        protected Quest(Guid id, bool complete, string description) : base(id)
        {
            Complete = complete;
            Description = description;
        }

        public bool Complete { get; protected set; }
        public string Description { get; }
    }
}