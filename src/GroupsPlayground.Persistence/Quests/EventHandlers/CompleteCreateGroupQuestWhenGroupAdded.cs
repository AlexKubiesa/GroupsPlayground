using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Groups;

namespace GroupsPlayground.Persistence.Quests.EventHandlers
{
    public sealed class CompleteCreateGroupQuestWhenGroupAdded : IHandler<GroupAddedEvent>
    {
        public void Handle(GroupAddedEvent @event)
        {
            var quest = QuestInstances.CreateGroupQuest;

            if (quest.Complete)
                return;

            using var session = new QuestsSession();
            quest.Complete = true;
            session.QuestRepository.UpdateQuest(quest);
            session.SaveChanges();
        }
    }
}