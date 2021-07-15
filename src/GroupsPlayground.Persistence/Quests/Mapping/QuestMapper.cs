using DomainModel = GroupsPlayground.Domain.Quests;
using PersistenceModel = GroupsPlayground.Persistence.Quests.Model;

namespace GroupsPlayground.Persistence.Quests.Mapping
{
    public static class QuestMapper
    {
        public static DomainModel.Quest ToDomain(PersistenceModel.Quest quest) =>
            DomainModel.Quest.Create(quest.Id, quest.Complete, quest.Description);

        public static PersistenceModel.Quest ToPersistence(DomainModel.Quest quest) =>
            new PersistenceModel.Quest(quest.Id) { Complete = quest.Complete, Description = quest.Description };
    }
}