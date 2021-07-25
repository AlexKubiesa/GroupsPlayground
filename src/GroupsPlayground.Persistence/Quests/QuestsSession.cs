using System;
using GroupsPlayground.Persistence.Common;

namespace GroupsPlayground.Persistence.Quests
{
    public class QuestsSession : AppSession
    {
        public QuestsSession()
        {
            AddRepository(QuestRepository = new QuestRepository(Context));
        }

        public QuestRepository QuestRepository { get; }

        public void EnsureReferenceData() => Context.AddReferenceData();
    }
}