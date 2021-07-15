using System;
using GroupsPlayground.Persistence.Common;
using GroupsPlayground.Persistence.Quests;
using Microsoft.EntityFrameworkCore;

namespace GroupsPlayground.Persistence
{
    public class DatabaseSession : IDisposable
    {
        private readonly AppDbContext context = new AppDbContext();

        public void Dispose() => context?.Dispose();

        public void RegenerateDatabase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}