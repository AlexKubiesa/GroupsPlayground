using System;
using GroupsPlayground.Domain.Quests;

namespace GroupsPlayground.Persistence.Quests
{
    internal static class QuestInstances
    {
        public static readonly Guid CreateGroupId = Guid.Parse("C5DA5525-2A22-4F1D-A8C6-1458A9D632F9");

        static QuestInstances()
        {
            using var session = new QuestsSession();
            CreateGroupQuest = session.QuestRepository.GetQuest(CreateGroupId);
        }

        public static Quest CreateGroupQuest { get; }
    }
}