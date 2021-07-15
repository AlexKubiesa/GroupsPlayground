using System;
using GroupsPlayground.Persistence.Framework;

namespace GroupsPlayground.Persistence.Quests.Model
{
    public class Quest : Entity
    {
        public Quest(Guid id) : base(id)
        {
        }

        public bool Complete { get; set; }

        public string Description { get; set; }
    }
}