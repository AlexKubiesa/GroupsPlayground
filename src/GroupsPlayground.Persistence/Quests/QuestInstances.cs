using System;
using GroupsPlayground.Domain.Quests;

namespace GroupsPlayground.Persistence.Quests
{
    internal static class QuestInstances
    {
        public static readonly Guid CreateGroupId = Guid.Parse("C5DA5525-2A22-4F1D-A8C6-1458A9D632F9");
        public static readonly Guid ComputeGroupSizeId = Guid.Parse("B4B6C007-0EB8-4C93-9B89-53EFDD5F6BF0");

        static QuestInstances()
        {
            using var session = new QuestsSession();
            CreateGroupQuest = session.QuestRepository.GetQuest(CreateGroupId);
            ComputeGroupSizeQuest = session.QuestRepository.GetQuest(ComputeGroupSizeId);
        }

        public static Quest CreateGroupQuest { get; }
        public static Quest ComputeGroupSizeQuest { get; }
    }
}