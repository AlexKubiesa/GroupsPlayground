using System;

namespace GroupsPlayground.Persistence.Groups
{
    public class Session : IDisposable
    {
        private readonly GroupsDbContext context = new GroupsDbContext();

        public Session()
        {
            GroupRepository = new GroupRepository(context);
        }

        public GroupRepository GroupRepository { get; }

        public void RegenerateDatabase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public void Dispose() => context?.Dispose();

        public void SaveChanges() => context.SaveChanges();
    }
}