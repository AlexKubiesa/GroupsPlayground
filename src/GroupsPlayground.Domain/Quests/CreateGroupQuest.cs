using System;

namespace GroupsPlayground.Domain.Quests
{
    internal sealed class CreateGroupQuest : Quest
    {
        public CreateGroupQuest(Guid id, bool complete) : base(id, complete)
        {
        }
    }
}