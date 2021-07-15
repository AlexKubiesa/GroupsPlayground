using GroupsPlayground.Persistence.Common;
using GroupsPlayground.Persistence.Quests.Model;
using Microsoft.EntityFrameworkCore;

namespace GroupsPlayground.Persistence.Quests
{
    internal sealed class QuestsDbContext : AppDbContext
    {
        public DbSet<Quest> Quests { get; set; }
    }
}