using DomainModel = GroupsPlayground.Domain.Quests;
using PersistenceModel = GroupsPlayground.Persistence.Quests.Model;

namespace GroupsPlayground.Persistence.Quests.Mapping
{
    public static class QuestMapper
    {
        public static DomainModel.Quest ToDomain(PersistenceModel.Quest quest) =>
            new DomainModel.Quest(quest.Id) { Complete = quest.Complete };

        public static PersistenceModel.Quest ToPersistence(DomainModel.Quest quest) =>
            new PersistenceModel.Quest(quest.Id) { Complete = quest.Complete };
    }
}