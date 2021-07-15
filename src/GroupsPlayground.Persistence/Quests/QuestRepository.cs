using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Quests;
using GroupsPlayground.Persistence.Quests.Mapping;

namespace GroupsPlayground.Persistence.Quests
{
    public sealed class QuestRepository
    {
        private readonly QuestsDbContext context;

        internal QuestRepository(QuestsDbContext context)
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
    }
}