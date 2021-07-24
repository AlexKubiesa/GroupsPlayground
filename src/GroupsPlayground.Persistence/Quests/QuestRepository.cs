using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Quests;
using GroupsPlayground.Persistence.Common;
using GroupsPlayground.Persistence.Quests.Mapping;

namespace GroupsPlayground.Persistence.Quests
{
    public sealed class QuestRepository
    {
        private readonly AppDbContext context;

        internal QuestRepository(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Quest GetQuest(Guid id) =>
            QuestMapper.ToDomain(context.Quests.Find(id));

        public List<Quest> GetAllQuests()
        {
            var quests = context.Quests.ToList();
            return quests.Select(QuestMapper.ToDomain).ToList();
        }

        public void UpdateQuest(Quest quest)
        {
            context.Quests.Update(QuestMapper.ToPersistence(quest));
            DomainEvents.DispatchEvents(quest);
        }
    }
}