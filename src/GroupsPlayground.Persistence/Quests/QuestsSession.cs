using System;
using GroupsPlayground.Persistence.Common;

namespace GroupsPlayground.Persistence.Quests
{
    public class QuestsSession : IDisposable
    {
        private readonly AppDbContext context = new AppDbContext();

        public QuestsSession()
        {
            QuestRepository = new QuestRepository(context);
        }

        public QuestRepository QuestRepository { get; }

        public void Dispose() => context?.Dispose();

        public void EnsureReferenceData() => context.AddReferenceData();

        public void SaveChanges() => context.SaveChanges();
    }
}