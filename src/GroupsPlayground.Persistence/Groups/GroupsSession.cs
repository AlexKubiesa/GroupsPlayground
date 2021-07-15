using System;
using GroupsPlayground.Persistence.Common;

namespace GroupsPlayground.Persistence.Groups
{
    public class GroupsSession : IDisposable
    {
        private readonly AppDbContext context = new AppDbContext();

        public GroupsSession()
        {
            GroupRepository = new GroupRepository(context);
        }

        public GroupRepository GroupRepository { get; }

        public void Dispose() => context?.Dispose();

        public void SaveChanges() => context.SaveChanges();
    }
}