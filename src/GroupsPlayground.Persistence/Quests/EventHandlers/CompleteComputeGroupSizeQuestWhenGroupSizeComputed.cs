using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Groups;

namespace GroupsPlayground.Persistence.Quests.EventHandlers
{
    public sealed class CompleteComputeGroupSizeQuestWhenGroupSizeComputed : IHandler<GroupSizeComputedEvent>
    {
        public void Handle(GroupSizeComputedEvent @event)
        {
            var quest = QuestInstances.ComputeGroupSizeQuest;

            if (quest.Complete)
                return;

            using var session = new QuestsSession();
            quest.Complete = true;
            session.QuestRepository.UpdateQuest(quest);
            session.SaveChanges();
        }
    }
}